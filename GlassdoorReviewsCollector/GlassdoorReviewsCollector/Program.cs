using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace GlassdoorReviewsCollector
{
    class Program
    {
        //Set program logger
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (!options.CheckParams())
                {
                    Console.WriteLine(options.GetUsage());
                }
                else
                {

                }
                Console.ReadKey();
            }

        }
    }
}
