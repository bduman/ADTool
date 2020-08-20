using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace ADTool.Tests.Utilities
{
    public class TestConsole : IConsole
    {
        public TestConsole()
        {
            Out = new TestTextWriter();
            Error = new TestTextWriter();
        }

        public TextWriter Out { get; set; }

        public TextWriter Error { get; set; }

        public TextReader In { get; set; }

        public bool IsInputRedirected => throw new NotImplementedException();

        public bool IsOutputRedirected => true;

        public bool IsErrorRedirected => true;

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public event ConsoleCancelEventHandler CancelKeyPress
        {
            add { }
            remove { }
        }

        public void ResetColor()
        {

        }
    }
}