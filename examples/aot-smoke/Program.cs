using TreeSitter;

// Language id, sample source. Each one has to parse without errors.
(string Id, string Source)[] samples =
[
    ("Go", "package main\nfunc main() { println(\"hi\") }\n"),
    ("Java", "class A { void run() { System.out.println(1); } }\n"),
    ("JavaScript", "function one() { function two() {} }\n"),
    ("Kotlin", "fun main() {\n    println(\"hi\")\n}\n"),
    ("Python", "def greet(name):\n    return 'hi ' + name\n"),
    ("Ruby", "def greet(name)\n  puts name\nend\n"),
    ("Rust", "fn main() { println!(\"hi\"); }\n"),
    ("Swift", "func greet(name: String) -> String {\n    return \"hi \" + name\n}\n"),
    ("TypeScript", "function f(a: number): number { return a; }\n"),
    ("TSX", "const x = <div className=\"a\">{1}</div>;\n"),
];

// Only the ids named on the command line, so a missing library can be told apart.
var wanted = args.Length > 0 ? new HashSet<string>(args, StringComparer.OrdinalIgnoreCase) : null;
var failed = 0;

foreach (var (id, source) in samples)
{
    if (wanted != null && !wanted.Contains(id))
    {
        continue;
    }

    try
    {
        using var language = new Language(id);
        using var parser = new Parser(language);
        using var tree = parser.Parse(source)!;
        var ok = !tree.RootNode.HasError;
        Console.WriteLine($"{(ok ? "ok  " : "FAIL")} {id}: {language.Name} abi {language.AbiVersion}, {tree.RootNode.DescendantCount} nodes");
        failed += ok ? 0 : 1;
    }
    catch (Exception e)
    {
        Console.WriteLine($"FAIL {id}: {e.Message}");
        failed++;
    }
}

if (wanted == null || wanted.Contains("JavaScript"))
{
    using var language = new Language("JavaScript");
    using var parser = new Parser(language);
    using var tree = parser.Parse("function one() { function two() {} }")!;
    using var query = new Query(language, "(function_declaration name: (identifier) @fn)");
    var names = query.Execute(tree.RootNode).Captures.Select(c => c.Node.Text).ToArray();
    var ok = names.SequenceEqual(["one", "two"]);
    Console.WriteLine($"{(ok ? "ok  " : "FAIL")} JavaScript query: {string.Join(", ", names)}");
    failed += ok ? 0 : 1;
}

if (wanted == null || wanted.Contains("Python"))
{
    using var language = new Language("Python");
    using var parser = new Parser(language);
    using var tree = parser.Parse("def a():\n    pass\n\ndef b():\n    pass\n")!;
    using var query = new Query(language, "(function_definition name: (identifier) @fn)");
    var names = query.Execute(tree.RootNode).Captures.Select(c => c.Node.Text).ToArray();
    var ok = names.SequenceEqual(["a", "b"]);
    Console.WriteLine($"{(ok ? "ok  " : "FAIL")} Python query: {string.Join(", ", names)}");
    failed += ok ? 0 : 1;
}

return failed == 0 ? 0 : 1;
