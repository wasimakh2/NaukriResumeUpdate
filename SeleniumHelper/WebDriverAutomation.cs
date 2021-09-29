using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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


        private By GetObj(string locatorType, string selector)
        {
            Dictionary<string, By> map = new Dictionary<string, By>();
            map.Add("ID", By.Id(selector));

            map.Add("NAME", By.Name(selector));
            map.Add("XPATH", By.XPath(selector));
            map.Add("TAG", By.TagName(selector));
            map.Add("CLASS", By.ClassName(selector));
            map.Add("CSS", By.CssSelector(selector));
            map.Add("LINKTEXT", By.LinkText(selector));

            return map[locatorType];
        }

        public IWebElement GetElement(string elementTag, string locator)
        {
            By _by = GetObj(locator, elementTag);

            if (IsElementPresent(_by))
            {
                return WebDriver.FindElement(_by);
            }

            return null;
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}