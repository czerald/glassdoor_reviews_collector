using System;

namespace GlassdoorReviewsCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.CheckParams())
                {
                    if (!String.IsNullOrEmpty(options.Location))
                    {
                        Console.WriteLine("Location: {0}", options.Location);
                    }
                }
                Console.ReadKey();
            }

        }
    }
}
