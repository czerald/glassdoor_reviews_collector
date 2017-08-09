using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace GlassdoorReviewsCollector
{
    class SearchPage
    {
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IWebDriver driver;
        private IWebElement elemCompanyNameTextbox;
        private IWebElement elemLocationTextbox;
        private IWebElement elemSearchButton;

        public SearchPage()
        {
            logger.Debug("Initializing ChromeDriver");
            driver = new ChromeDriver();
        }

        public void NavigateHome()
        {
            logger.Debug("Navigating to home page");
            driver.Navigate().GoToUrl(Properties.Settings.Default.homepage);
            Thread.Sleep(2000);
        }

        public bool LocateForm()
        {
            logger.Debug("Finding form elements");
            string sCompanyNameSelector = Properties.Settings.Default.company_name_selector;
            string sLocationSelector = Properties.Settings.Default.location_selector;
            string sSearchButton = Properties.Settings.Default.search_button_selector;

            elemCompanyNameTextbox = driver.FindElement(By.CssSelector(sCompanyNameSelector));
            elemLocationTextbox = driver.FindElement(By.CssSelector(sLocationSelector));
            elemSearchButton = driver.FindElement(By.CssSelector(sSearchButton));

            logger.Debug("Checking if the required elements are loaded in the home page");
            if (!elemCompanyNameTextbox.Displayed || !elemLocationTextbox.Displayed || !elemSearchButton.Displayed)
            {
                logger.Error("One or more elements in page is not displayed.");
                logger.Debug("Element " + sCompanyNameSelector + ": " + elemCompanyNameTextbox.Displayed);
                logger.Debug("Element " + sLocationSelector + ": " + elemLocationTextbox.Displayed);
                logger.Debug("Element " + sSearchButton + ": " + elemSearchButton.Displayed);
                return false;
            }
            else
            {
                logger.Debug("Elements found.");
                return true;
            }
        }

        public bool Search(string Location="", string Company="")
        {
            if (!String.IsNullOrEmpty(Company) || !String.IsNullOrEmpty(Location))
            {
                if (!String.IsNullOrEmpty(Company))
                {
                    logger.Debug("Setting value to company textbox");
                    elemCompanyNameTextbox.SendKeys(Company);
                }

                if (!String.IsNullOrEmpty(Location))
                {
                    logger.Debug("Setting value to location textbox");
                    elemLocationTextbox.SendKeys(Location);
                }

                logger.Debug("Searching...");
                elemSearchButton.Click();
                Thread.Sleep(5000);
                return true;
            }
            else
                logger.Error("Missing program argument");
                return false;
        }

        public void Dispose()
        {
            logger.Info("Exiting browser");
            driver.Dispose();
        }
    }
}
