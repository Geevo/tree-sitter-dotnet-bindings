//
// .NET bindings for tree-sitter
// Copyright (c) 2025 Marius Greuel
// SPDX-License-Identifier: MIT
// https://github.com/mariusgreuel/tree-sitter-dotnet-bindings
//

using System.Reflection;
using System.Runtime.InteropServices;

namespace TreeSitter;

/// <summary>
/// Finds the tree-sitter native libraries, either next to the app or in runtimes/&lt;rid&gt;/native.
/// </summary>
internal static class NativeLibraryResolver
{
    internal static void Register()
    {
        // The runtime only probes the app directory on its own, so the runtimes folder needs a resolver.
        NativeLibrary.SetDllImportResolver(typeof(NativeLibraryResolver).Assembly, Resolve);
    }

    internal static IntPtr Load(string libraryName)
    {
        if (TryLoad(libraryName, out var handle))
        {
            return handle;
        }

        throw new DllNotFoundException($"Unable to load shared library '{libraryName}' or one of its dependencies.");
    }

    static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        return TryLoad(libraryName, out var handle) ? handle : IntPtr.Zero;
    }

    static bool TryLoad(string libraryName, out IntPtr handle)
    {
        var fileName = Path.GetFileName(libraryName);
        var directory = Path.GetDirectoryName(libraryName);

        foreach (var variation in GetFileNameVariations(fileName))
        {
            if (!string.IsNullOrEmpty(directory))
            {
                if (TryLoadPath(Path.Combine(directory, variation), out handle))
                {
                    return true;
                }

                continue;
            }

            if (TryLoadPath(Path.Combine(AppContext.BaseDirectory, variation), out handle)
                || TryLoadPath(Path.Combine(AppContext.BaseDirectory, "runtimes", GetRuntimeIdentifier(), "native", variation), out handle))
            {
                return true;
            }
        }

        if (string.IsNullOrEmpty(directory))
        {
            // Fall back to the system search path
            foreach (var variation in GetFileNameVariations(fileName))
            {
                if (TryLoadPath(variation, out handle))
                {
                    return true;
                }
            }
        }

        handle = IntPtr.Zero;
        return false;
    }

    static bool TryLoadPath(string path, out IntPtr handle)
    {
        if (s_debug)
        {
            Console.WriteLine($"[TREESITTER_DEBUG_LOADER] Loading library: {path}");
        }

        return NativeLibrary.TryLoad(path, out handle);
    }

    static IEnumerable<string> GetFileNameVariations(string name)
    {
        string prefix, suffix;
        if (OperatingSystem.IsWindows())
        {
            prefix = string.Empty;
            suffix = ".dll";
        }
        else if (OperatingSystem.IsMacOS())
        {
            prefix = "lib";
            suffix = ".dylib";
        }
        else
        {
            prefix = "lib";
            suffix = ".so";
        }

        if (!name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
        {
            yield return prefix + name + suffix;
        }

        yield return name;
    }

    static string GetRuntimeIdentifier()
    {
        var architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        if (OperatingSystem.IsWindows())
        {
            return "win-" + architecture;
        }

        if (OperatingSystem.IsMacOS())
        {
            return "osx-" + architecture;
        }

        if (OperatingSystem.IsLinux())
        {
            return "linux-" + architecture;
        }

        throw new PlatformNotSupportedException();
    }

    static readonly bool s_debug = Environment.GetEnvironmentVariable("TREESITTER_DEBUG_LOADER") == "1";
}
