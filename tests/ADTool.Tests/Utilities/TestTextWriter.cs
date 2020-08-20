using System.IO;
using System.Text;

namespace ADTool.Tests.Utilities
{
    public class TestTextWriter : TextWriter
    {
        private readonly StringBuilder _sb = new StringBuilder();
        public override Encoding Encoding => Encoding.Unicode;

        public override void Write(char ch)
        {
            _sb.Append(ch);
        }

        public override void Flush()
        {
            _sb.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sb.Length > 0)
                {
                    _sb.Clear();
                }
            }

            base.Dispose(disposing);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}