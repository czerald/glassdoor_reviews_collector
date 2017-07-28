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
                    logger.Debug("Initializing ChromeDriver");
                    IWebDriver driver = new ChromeDriver();

                    logger.Debug("Navigating to home page");
                    driver.Navigate().GoToUrl(Properties.Settings.Default.homepage);
                    Thread.Sleep(2000);

                    logger.Debug("Finding form elements");
                    string sCompanyNameSelector = Properties.Settings.Default.company_name_selector;
                    string sLocationSelector = Properties.Settings.Default.location_selector;
                    string sSearchButton = Properties.Settings.Default.search_button_selector;

                    IWebElement elemCompanyNameTextbox = driver.FindElement(By.CssSelector(sCompanyNameSelector));
                    IWebElement elemLocationTextbox = driver.FindElement(By.CssSelector(sLocationSelector));
                    IWebElement elemSearchButton = driver.FindElement(By.CssSelector(sSearchButton));

                    logger.Debug("Checking if the required elements are loaded in the home page");
                    if (!elemCompanyNameTextbox.Displayed || !elemLocationTextbox.Displayed || !elemSearchButton.Displayed)
                    {
                        logger.Error("One or more elements in page is not displayed.");
                        logger.Debug("Element " + sCompanyNameSelector + ": " + elemCompanyNameTextbox.Displayed);
                        logger.Debug("Element " + sLocationSelector + ": " + elemLocationTextbox.Displayed);
                        logger.Debug("Element " + sSearchButton + ": " + elemSearchButton.Displayed);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(options.Company))
                        {
                            logger.Debug("Setting value to company textbox");
                            elemCompanyNameTextbox.SendKeys(options.Company);
                        }

                        if (!String.IsNullOrEmpty(options.Location))
                        {
                            logger.Debug("Setting value to location textbox");
                            elemLocationTextbox.SendKeys(options.Location);
                        }

                        Console.ReadKey();
                        elemSearchButton.Click();
                        Thread.Sleep(5000);
                    }
                }
                Console.ReadKey();
            }

        }
    }
}
