namespace ADTool.Models
{
    public class AssemblyData
    {
        public string AssemblyFilePath { get; }
        public string SourceCode { get; set; }

        public AssemblyData(string assemblyFilePath)
        {
            this.AssemblyFilePath = assemblyFilePath;
        }
    }
}