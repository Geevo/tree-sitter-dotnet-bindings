//
// .NET bindings for tree-sitter
// Copyright (c) 2025 Marius Greuel
// SPDX-License-Identifier: MIT
// https://github.com/mariusgreuel/tree-sitter-dotnet-bindings
//

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace TreeSitter;

#pragma warning disable IDE1006 // Naming Styles

internal static unsafe partial class Native
{
    internal static class Libraries
    {
        internal const string TreeSitter = "tree-sitter";
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSPoint
    {
        internal uint row;
        internal uint column;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSRange
    {
        internal TSPoint start_point;
        internal TSPoint end_point;
        internal uint start_byte;
        internal uint end_byte;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSInput
    {
        internal IntPtr payload;
        internal IntPtr read;
        internal InputEncoding encoding;
        internal IntPtr decode;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSParseState
    {
        internal IntPtr payload;
        internal uint current_byte_offset;
        [MarshalAs(UnmanagedType.I1)]
        internal bool has_error;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSParseOptions
    {
        internal IntPtr payload;
        internal IntPtr progress_callback;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSLogger
    {
        internal IntPtr payload;
        internal delegate* unmanaged[Cdecl]<IntPtr, LogType, byte*, void> log;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSInputEdit
    {
        internal uint start_byte;
        internal uint old_end_byte;
        internal uint new_end_byte;
        internal TSPoint start_point;
        internal TSPoint old_end_point;
        internal TSPoint new_end_point;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSNode
    {
        internal uint context0;
        internal uint context1;
        internal uint context2;
        internal uint context3;
        internal IntPtr id;
        internal IntPtr tree;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSTreeCursor
    {
        internal IntPtr tree;
        internal IntPtr id;
        internal uint context0;
        internal uint context1;
        internal uint context2;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSQueryCapture
    {
        internal TSNode node;
        internal uint index;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSQueryMatch
    {
        internal uint id;
        internal ushort pattern_index;
        internal ushort capture_count;
        internal IntPtr captures;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSQueryPredicateStep
    {
        internal QueryPredicateStepType type;
        internal uint value_id;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSQueryCursorState
    {
        internal IntPtr payload;
        internal uint current_byte_offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TSQueryCursorOptions
    {
        internal IntPtr payload;
        internal IntPtr progress_callback;
    }

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_parser_new();

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_parser_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_parser_language(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_parser_set_language(IntPtr self, IntPtr language);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_parser_set_included_ranges(IntPtr self, ReadOnlySpan<TSRange> ranges, uint count);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_parser_included_ranges(IntPtr self, out uint count);

    [LibraryImport(Libraries.TreeSitter, StringMarshalling = StringMarshalling.Utf16)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_parser_parse_string_encoding(IntPtr self, IntPtr old_tree, string text, uint length, InputEncoding encoding);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_parser_reset(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_parser_set_logger(IntPtr self, TSLogger logger);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_tree_copy(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_tree_root_node(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_tree_root_node_with_offset(IntPtr self, uint offset_bytes, TSPoint offset_extent);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_tree_language(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_tree_included_ranges(IntPtr self, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_edit(IntPtr self, ref TSInputEdit edit);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_tree_get_changed_ranges(IntPtr old_tree, IntPtr new_tree, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_print_dot_graph(IntPtr self, IntPtr file_descriptor);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_type(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_node_symbol(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_language(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_grammar_type(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_node_grammar_symbol(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_node_start_byte(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSPoint ts_node_start_point(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_node_end_byte(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSPoint ts_node_end_point(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_string(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_is_null(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_is_named(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_is_missing(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_is_extra(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_has_changes(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_has_error(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_is_error(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_node_parse_state(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_node_next_parse_state(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_parent(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_child_with_descendant(TSNode self, TSNode descendant);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_child(TSNode self, uint child_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_field_name_for_child(TSNode self, uint child_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_node_field_name_for_named_child(TSNode self, uint named_child_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_node_child_count(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_named_child(TSNode self, uint child_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_node_named_child_count(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_child_by_field_name(TSNode self, ReadOnlySpan<byte> name, uint name_length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_child_by_field_id(TSNode self, ushort field_id);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_next_sibling(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_prev_sibling(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_next_named_sibling(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_prev_named_sibling(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_first_child_for_byte(TSNode self, uint byte_offset);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_first_named_child_for_byte(TSNode self, uint byte_offset);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_node_descendant_count(TSNode self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_descendant_for_byte_range(TSNode self, uint start, uint end);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_descendant_for_point_range(TSNode self, TSPoint start, TSPoint end);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_named_descendant_for_byte_range(TSNode self, uint start, uint end);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_node_named_descendant_for_point_range(TSNode self, TSPoint start, TSPoint end);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_node_edit(TSNode self, ref TSInputEdit edit);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_node_eq(TSNode self, TSNode other);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSTreeCursor ts_tree_cursor_new(TSNode node);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_cursor_delete(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_cursor_reset(ref TSTreeCursor self, TSNode node);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_tree_cursor_reset_to(ref TSTreeCursor dst, ref TSTreeCursor src);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSNode ts_tree_cursor_current_node(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_tree_cursor_current_field_name(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_tree_cursor_current_field_id(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_tree_cursor_goto_parent(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_tree_cursor_goto_next_sibling(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_tree_cursor_goto_previous_sibling(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_tree_cursor_goto_first_child(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_tree_cursor_goto_last_child(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial void ts_tree_cursor_goto_descendant(ref TSTreeCursor self, uint goal_descendant_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_tree_cursor_current_descendant_index(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_tree_cursor_current_depth(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long ts_tree_cursor_goto_first_child_for_byte(ref TSTreeCursor self, uint goal_byte);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long ts_tree_cursor_goto_first_child_for_point(ref TSTreeCursor self, TSPoint goal_point);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial TSTreeCursor ts_tree_cursor_copy(ref TSTreeCursor self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_query_new(IntPtr language, ReadOnlySpan<byte> source, uint source_len, out uint error_offset, out QueryError error_type);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_pattern_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_capture_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_string_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_start_byte_for_pattern(IntPtr self, uint pattern_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_end_byte_for_pattern(IntPtr self, uint pattern_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_query_predicates_for_pattern(IntPtr self, uint pattern_index, out uint step_count);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_is_pattern_rooted(IntPtr self, uint pattern_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_is_pattern_non_local(IntPtr self, uint pattern_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_is_pattern_guaranteed_at_step(IntPtr self, uint byte_offset);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_query_capture_name_for_id(IntPtr self, uint index, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Quantifier ts_query_capture_quantifier_for_id(IntPtr self, uint pattern_index, uint capture_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_query_string_value_for_id(IntPtr self, uint index, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_disable_capture(IntPtr self, ReadOnlySpan<byte> name, uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_disable_pattern(IntPtr self, uint pattern_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_query_cursor_new();

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_cursor_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_cursor_exec(IntPtr self, IntPtr query, TSNode node);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_cursor_exec_with_options(IntPtr self, IntPtr query, TSNode node, ref TSQueryCursorOptions query_options);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_cursor_did_exceed_match_limit(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_query_cursor_match_limit(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_cursor_set_match_limit(IntPtr self, uint limit);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_cursor_set_byte_range(IntPtr self, uint start_byte, uint end_byte);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_cursor_set_point_range(IntPtr self, TSPoint start_point, TSPoint end_point);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_cursor_next_match(IntPtr self, out TSQueryMatch match);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_query_cursor_remove_match(IntPtr self, uint match_id);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_query_cursor_next_capture(IntPtr self, out TSQueryMatch match, out uint capture_index);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_copy(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_language_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_language_symbol_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_language_state_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_language_symbol_for_name(IntPtr self, ReadOnlySpan<byte> name, uint name_length, [MarshalAs(UnmanagedType.U1)] bool is_named);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_language_field_count(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_field_name_for_id(IntPtr self, ushort id);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_language_field_id_for_name(IntPtr self, ReadOnlySpan<byte> name, uint name_length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_supertypes(IntPtr self, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_subtypes(IntPtr self, ushort supertype, out uint length);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_symbol_name(IntPtr self, ushort symbol);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SymbolType ts_language_symbol_type(IntPtr self, ushort symbol);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ts_language_abi_version(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_language_next_state(IntPtr self, ushort state, ushort symbol);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_language_name(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_lookahead_iterator_new(IntPtr language, ushort state);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ts_lookahead_iterator_delete(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_lookahead_iterator_reset_state(IntPtr self, ushort state);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_lookahead_iterator_reset(IntPtr self, IntPtr language, ushort state);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_lookahead_iterator_language(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ts_lookahead_iterator_next(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ushort ts_lookahead_iterator_current_symbol(IntPtr self);

    [LibraryImport(Libraries.TreeSitter)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr ts_lookahead_iterator_current_symbol_name(IntPtr self);

    internal static void ts_free(IntPtr ptr)
    {
        s_ts_current_free(ptr);
    }

    internal static string? MarshalString(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr);
    }

    internal static string? MarshalString(IntPtr ptr, int length)
    {
        return Marshal.PtrToStringUTF8(ptr, length);
    }

    internal static T MarshalArrayItem<T>(IntPtr array, int index) where T : unmanaged
    {
        return ((T*)array)[index];
    }

    internal static ReadOnlySpan<byte> ToUTF8(string text, out uint length)
    {
        var buffer = Encoding.UTF8.GetBytes(text);
        length = (uint)buffer.Length;
        return buffer;
    }

    internal static uint IntToUInt(int value)
    {
        return value >= 0 && value < int.MaxValue ? (uint)value : uint.MaxValue;
    }

    internal static int UIntToInt(uint value)
    {
        return value < int.MaxValue ? (int)value : int.MaxValue;
    }

    internal static int ByteToIndex(uint value)
    {
        return value < int.MaxValue ? (int)(value / 2) : int.MaxValue;
    }

    internal static uint IndexToByte(int value)
    {
        return value >= 0 && value < int.MaxValue ? (uint)value * 2 : uint.MaxValue;
    }

    static delegate* unmanaged[Cdecl]<IntPtr, void> GetFreeFunction()
    {
        var handle = NativeLibraryResolver.Load(Libraries.TreeSitter);
        // ts_current_free is a global function pointer variable, not a function
        var ts_current_free = NativeLibrary.GetExport(handle, "ts_current_free");
        return (delegate* unmanaged[Cdecl]<IntPtr, void>)Marshal.ReadIntPtr(ts_current_free);
    }

    static readonly delegate* unmanaged[Cdecl]<IntPtr, void> s_ts_current_free;

    static Native()
    {
        // Has to be in place before the first P/Invoke into the library.
        NativeLibraryResolver.Register();
        s_ts_current_free = GetFreeFunction();
    }
}
