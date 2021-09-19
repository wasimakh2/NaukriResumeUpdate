using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumHelper
{
    public class WebDriverAutomation
    {
        public IWebDriver WebDriver { get; set; }

        public WebDriverAutomation()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-notifications");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popups");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-dev-shm-usage");
            //options.AddArgument("--window-position=-32000,-32000");

            //options.AddArgument("headless");

            //Disable webdriver flags or you will be easily detectable
            options.AddArgument("--disable-blink-features");
            options.AddArgument("--disable-blink-features=AutomationControlled");

            new DriverManager().SetUpDriver(new ChromeConfig());
            WebDriver = new ChromeDriver(options);

            SetImplicitWait(10);
        }

        private void SetImplicitWait(int time)
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }

        public string GetPageSource(string URL)
        {
            WebDriver.Navigate()
                     .GoToUrl(URL);
            string pagesource = WebDriver.PageSource;

            return pagesource;
        }

        public void TearDown()
        {
            try
            {
                WebDriver.Close();
                Console.WriteLine("Driver Closed Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }

            try
            {
                WebDriver.Quit();

                Console.WriteLine("Driver Quit Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }
        }
    }
}