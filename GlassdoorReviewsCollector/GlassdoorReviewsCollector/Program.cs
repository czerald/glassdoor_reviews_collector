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
                    SearchPage sp = new SearchPage();
                    sp.NavigateHome();
                    if(sp.LocateForm())
                    {
                        if(string.IsNullOrEmpty(options.Company) && !string.IsNullOrEmpty(options.Location))
                            sp.Search(Location: options.Location);
                        else if (!string.IsNullOrEmpty(options.Company) && string.IsNullOrEmpty(options.Location))
                            sp.Search(Company: options.Company);
                        else if (!string.IsNullOrEmpty(options.Company) && !string.IsNullOrEmpty(options.Location))
                            sp.Search(options.Location, options.Company);
                    }
                    sp.Dispose();
                }
                Console.WriteLine("Program finished. Press any key to exit.");
                Console.ReadKey();
            }

        }
    }
}
