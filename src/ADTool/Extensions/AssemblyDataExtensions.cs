using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ADTool.Models;

namespace ADTool.Extensions
{
    public static class AssemblyDataExtensions
    {
        private const string AssemblyTagPattern = @"\[assembly.*?\]";
        private const string EmptyLinePattern = @"^\s+$[\r\n]*";

        public static void RemoveAssemblyTags(this AssemblyData assembly)
        {
            var result = assembly.SourceCode;

            result = Regex.Replace(result, AssemblyTagPattern, string.Empty, RegexOptions.IgnorePatternWhitespace);
            result = Regex.Replace(result, EmptyLinePattern, string.Empty, RegexOptions.Multiline);

            assembly.SourceCode = result;
        }

        public static IEnumerable<string> GetAssemblyTags(this AssemblyData assembly)
        {
            var matches = Regex.Matches(assembly.SourceCode, AssemblyTagPattern, RegexOptions.IgnorePatternWhitespace);

            return matches.Select(m => m.Groups[0].Value);
        }
    }
}