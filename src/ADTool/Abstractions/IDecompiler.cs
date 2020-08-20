namespace ADTool.Abstractions
{
    public interface IDecompiler
    {
        string Decompile(string assemblyFileName);
    }
}