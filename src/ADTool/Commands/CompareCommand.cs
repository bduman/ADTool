using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ADTool.Abstractions;
using ADTool.Extensions;
using ADTool.Models;
using McMaster.Extensions.CommandLineUtils;

namespace ADTool.Commands
{
    [Command("compare", Description = "Compare 2 assembly")]
    class CompareCommand
    {
        [Required(ErrorMessage = "You must specify the first assembly path")]
        [Argument(0, Description = "First Assembly Path")]
        private string FirstAssemblyPath { get; }

        [Required(ErrorMessage = "You must specify the second assembly path")]
        [Argument(1, Description = "Second Assembly Path")]
        private string SecondAssemblyPath { get; }

        [Option("-o|--Output")]
        public bool Output { get; }

        [Option("-t|--WithOutAssemblyTags")]
        public bool WithoutAssemblyTags { get; }

        private async Task OnExecute(CommandLineApplication app, IDecompiler decompiler, HashAlgorithm hashAlgorithm)
        {
            var isFailed = false;

            app.Out.WriteLine(app.WorkingDirectory);

            if (!File.Exists(FirstAssemblyPath))
            {
                app.Out.WriteLine(FirstAssemblyPath + " file not exists.");
                return;
            }

            if (!File.Exists(SecondAssemblyPath))
            {
                app.Out.WriteLine(SecondAssemblyPath + " file not exists.");
                return;
            }

            var leftAssembly = new AssemblyData(FirstAssemblyPath);
            var rightAssembly = new AssemblyData(SecondAssemblyPath);

            leftAssembly.SourceCode = decompiler.Decompile(FirstAssemblyPath);
            rightAssembly.SourceCode = decompiler.Decompile(SecondAssemblyPath);

            if (this.WithoutAssemblyTags)
            {
                leftAssembly.RemoveAssemblyTags();
                rightAssembly.RemoveAssemblyTags();
            }
            else
            {
                // assembly tags not same order per build
                var firstAsemblyTags = leftAssembly.GetAssemblyTags();
                var secondAssemblyTags = rightAssembly.GetAssemblyTags();

                if (!firstAsemblyTags.IsEqual(secondAssemblyTags))
                {
                    app.Out.WriteLine("Assembly tags not equal");
                    app.Out.PrintCompareResult(false);
                    isFailed = true;
                }
            }

            if (this.Output)
            {
                await Task.WhenAll(this.WriteOutputAsync(leftAssembly),
                    this.WriteOutputAsync(rightAssembly));
            }

            if (isFailed)
            {
                return;
            }

            var firstHash = hashAlgorithm.ComputeHash(leftAssembly.SourceCode);
            var secondHash = hashAlgorithm.ComputeHash(rightAssembly.SourceCode);

            app.Out.WriteLine(FirstAssemblyPath + " Hash = " + firstHash);
            app.Out.WriteLine(SecondAssemblyPath + " Hash = " + secondHash);

            app.Out.PrintCompareResult(firstHash == secondHash);
        }

        private async Task WriteOutputAsync(AssemblyData assembly)
        {
            var output = Path.GetFileNameWithoutExtension(assembly.AssemblyFilePath) + ".txt";
            await File.WriteAllTextAsync(output, assembly.SourceCode);
        }
    }
}