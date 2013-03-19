using System;
using System.IO;
using System.Linq;

namespace VSSCRemove
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            prog.Go(args);
        }

        private string _directory;
  
        private void Go(string[] args)
        {
            if (!ParseArgs(args))
                return;

            var scanner = new Scanner(_directory);
            var remover = new Remover();
            try
            {
                foreach (var filename in scanner.Scan())
                {
                    Console.WriteLine(filename);
                    remover.Remove(filename);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private bool ParseArgs(string[] args)
        {
        	if (args.Length == 0 || !Directory.Exists(args[0]))
            {
            	PrintArgs();
                return false;
            }
            else
            {
            	_directory = args[0];
                return true;
            }
        }

        private void PrintArgs()
        {
            Console.WriteLine("Usage: VSSCRemove <directory>");
        }
    }
}
