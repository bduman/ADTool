using ADTool.Commands;
using ADTool.Tests.Utilities;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;

namespace ADTool.Tests
{
    public class CompareAssembliesTests
    {
        private CommandLineApplication<ADTCommand> Application;
        private TestConsole Console = new TestConsole();

        [SetUp]
        public void Setup()
        {
            var serviceProvider = ADTool.Program.GetServiceProvider(Console);
            this.Application = ADTool.Program.GetApplication(serviceProvider);
        }

        [Test]
        public void Same_Code_Include_Assembly_Tags_Result_Equal()
        {
            var args = new string[] { "compare", "resources/HelloWorldConsoleApp_Debug.dll", "resources/HelloWorldConsoleApp_Debug.dll" };
            this.Application.Execute(args);

            var output = Console.Out.ToString();

            Assert.True(output.Contains("Compare Result = EQUAL"));
        }

        [Test]
        public void Same_Code_Include_Assembly_Tags_Result_NEqual()
        {
            var args = new string[] { "compare", "resources/HelloWorldConsoleApp_Debug.dll", "resources/HelloWorldConsoleApp_Release.dll" };
            this.Application.Execute(args);

            var output = Console.Out.ToString();

            Assert.True(output.Contains("Compare Result = NOT EQUAL"));
        }

        [Test]
        public void Same_Code_Not_Include_Assembly_Tags_Result_Equal()
        {
            var args = new string[] { "compare", "-t", "resources/HelloWorldConsoleApp_Debug.dll", "resources/HelloWorldConsoleApp_Release.dll" };
            this.Application.Execute(args);

            var output = Console.Out.ToString();

            Assert.True(output.Contains("Compare Result = EQUAL"));
        }

        [Test]
        public void Not_Same_Code_Not_Include_Assembly_Tags_Result_NEqual()
        {
            var args = new string[] { "compare", "-t", "resources/HelloWorldConsoleApp_Release.dll", "resources/HelloWorldConsoleApp_Release_Diff.dll" };
            this.Application.Execute(args);

            var output = Console.Out.ToString();

            Assert.True(output.Contains("Compare Result = NOT EQUAL"));
        }

        [Test]
        public void Not_Same_Code_Include_Assembly_Tags_Result_NEqual()
        {
            var args = new string[] { "compare", "resources/HelloWorldConsoleApp_Release.dll", "resources/HelloWorldConsoleApp_Release_Diff.dll" };
            this.Application.Execute(args);

            var output = Console.Out.ToString();

            Assert.True(output.Contains("Compare Result = NOT EQUAL"));
        }

        [TearDown]
        public void AfterEachTest()
        {
            Console.Out.Flush();
        }
    }
}