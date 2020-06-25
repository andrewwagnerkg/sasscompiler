using System.IO;

namespace sasscompiler
{
    public enum OutputStyle
    {
        Nested = 0,
        Compressed,
        Compact,
        Expanded
    }

    internal class Compiler
    {
        LibSass.Compiler.Options.SassOptions options;

        private string OutputPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "index.css");

        private Compiler(string path)
        {
            options = new LibSass.Compiler.Options.SassOptions();
            options.InputPath = path;
        }

        public Compiler(string path, OutputStyle style) : this(path)
        {
            switch (style)
            {
                case OutputStyle.Nested:
                    options.OutputStyle = LibSass.Compiler.Options.SassOutputStyle.Nested;
                    break;
                case OutputStyle.Expanded:
                    options.OutputStyle = LibSass.Compiler.Options.SassOutputStyle.Expanded;
                    break;
                case OutputStyle.Compressed:
                    options.OutputStyle = LibSass.Compiler.Options.SassOutputStyle.Compressed;
                    break;
                case OutputStyle.Compact:
                    options.OutputStyle = LibSass.Compiler.Options.SassOutputStyle.Compact;
                    break;
                default:
                    options.OutputStyle = LibSass.Compiler.Options.SassOutputStyle.Compressed;
                    break;
            }
        }

        public Compiler(string path, OutputStyle style, string OutputPath) : this(path, style)
        {
            this.OutputPath = OutputPath;
        }

        public string Compile()
        {
            LibSass.Compiler.SassResult result=null;
            try
            {
                LibSass.Compiler.SassCompiler compiler = new LibSass.Compiler.SassCompiler(options);
                result = compiler.Compile();
            }
            catch
            {

            }
            return result.Output;
        }

        public void CompileToFile()
        {
            try
            {
                File.WriteAllText(OutputPath, Compile());
            }
            catch
            {

            }
        }
    }
}
