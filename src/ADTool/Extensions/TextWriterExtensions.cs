using System.IO;

namespace ADTool.Extensions
{
    public static class TextWriterExtensions
    {
        public static void PrintCompareResult(this TextWriter writer, bool equal)
        {
            writer.WriteLine("Compare Result = {0}", equal ? "EQUAL" : "NOT EQUAL");
        }
    }
}