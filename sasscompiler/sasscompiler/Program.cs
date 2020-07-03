using System;
using System.Threading;

namespace sasscompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string inputfilepath = string.Empty;
            while(string.IsNullOrEmpty(inputfilepath))
            {
                Console.WriteLine("Enter scss file path please:");
                inputfilepath=Console.ReadLine();
            }

            
            string outputfilepath = string.Empty;
            while (string.IsNullOrEmpty(outputfilepath))
            {
                Console.WriteLine("Enter css file path please:");
                outputfilepath = Console.ReadLine();
            }

            Thread task = new Thread(()=> { 
                start(inputfilepath, outputfilepath); 
            });
            task.Start();
            Console.WriteLine("Process is started...");
            Console.ReadKey();
        }

        private static void start(string InParam, string OutParam)
        {
            string temp = string.Empty;
            Compiler compiler = new Compiler(InParam, OutputStyle.Compact, OutParam);
            temp = compiler.Compile();
            while (true)
            {
                if (temp != compiler.Compile())
                {
                    temp = compiler.Compile();
                    Console.WriteLine($"{OutParam} >> {temp}");
                    compiler.CompileToFile();
                }
                Thread.Sleep(5);
            }
        }
    }
}
