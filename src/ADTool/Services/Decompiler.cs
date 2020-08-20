using System.IO;
using ADTool.Abstractions;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;

namespace ADTool.Services
{
    public class Decompiler : IDecompiler
    {
        private DecompilerSettings _decompilerSettings;

        public Decompiler()
        {
            this._decompilerSettings = GetSettings();
        }

        public string Decompile(string assemblyFileName)
        {
            var output = new StringWriter();
            var decompiler = GetDecompiler(assemblyFileName);

            output.Write(decompiler.DecompileWholeModuleAsString());

            return output.ToString();
        }

        private CSharpDecompiler GetDecompiler(string assemblyFileName)
        {
            var module = new PEFile(assemblyFileName);
            var resolver = new UniversalAssemblyResolver(assemblyFileName, false, module.Reader.DetectTargetFrameworkId());

            return new CSharpDecompiler(assemblyFileName, resolver, this._decompilerSettings)
            {
                DebugInfoProvider = null
            };
        }

        private DecompilerSettings GetSettings()
        {
            return new DecompilerSettings(LanguageVersion.Latest)
            {
                ThrowOnAssemblyResolveErrors = false,
                RemoveDeadCode = false,
                RemoveDeadStores = false
            };
        }
    }
}