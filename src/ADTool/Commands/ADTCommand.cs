using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace ADTool.Commands
{
    [Subcommand(
        typeof(CompareCommand)
    )]
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    public class ADTCommand
    {
        private void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }

        private static string GetVersion()
            => typeof(ADTCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}