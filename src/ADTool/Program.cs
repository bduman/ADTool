using System.Security.Cryptography;
using System.Threading.Tasks;
using ADTool.Abstractions;
using ADTool.Commands;
using ADTool.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace ADTool
{
    public static class Program
    {
        static async Task<int> Main(string[] args)
        {
            var serviceProvider = GetServiceProvider(PhysicalConsole.Singleton);
            var app = GetApplication(serviceProvider);

            return await app.ExecuteAsync(args);
        }

        public static CommandLineApplication<ADTCommand> GetApplication(ServiceProvider serviceProvider)
        {
            var app = new CommandLineApplication<ADTCommand>(serviceProvider.GetRequiredService<IConsole>());

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);

            return app;
        }

        public static ServiceProvider GetServiceProvider(IConsole consoleInstance)
        {
            return new ServiceCollection()
                .AddSingleton<IDecompiler, Decompiler>()
                .AddSingleton<HashAlgorithm, SHA256Managed>()
                .AddSingleton<IConsole>(consoleInstance)
                .BuildServiceProvider();
        }
    }
}
