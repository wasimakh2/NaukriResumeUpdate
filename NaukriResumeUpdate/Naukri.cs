using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NaukriResumeUpdate
{
    public class Naukri
    {
        private IWebDriver _webDriver;

        public string originalResumePath { get; set; } = @"C:\Users\Admin\Downloads\Documents\WASIM_AKHTAR_VisualCV_Resume.pdf";

        public string modifiedResumePath { get; set; } = @"C:\Users\Admin\Downloads\Documents\WASIM_AKHTAR_VisualCV_Resume.pdf";

        public string UserName { get; set; } = "Wasim.akh2@gmail.com";

        public string Password { get; set; } = "System@6";

        public string MobileNumber { get; set; } = "9718742936";

        public bool UpdatePDF { get; set; } = true;

        public string NaukriURL { get; set; } = @"https://login.naukri.com/nLogin/Login.php";




        public Naukri()
        {

            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-notifications");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popups");
            options.AddArgument("--disable-gpu");

            options.AddArgument("--disable-dev-shm-usage");
            //options.AddArgument("headless");
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver(options);

            _webDriver.Navigate().GoToUrl(NaukriURL);

            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        public By GetObj(string locatorType,string selector)
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

        public IWebElement GetElement(string elementTag,string locator)
        {
            By _by = GetObj(elementTag, locator);


            if(IsElementPresent(_by))
            {
                return _webDriver.FindElement(_by);
            }

            return null;
        }

        public void Login()
        {
            bool Status = false;
            IWebElement emailFieldElement;
            IWebElement passFieldElement;
            String loginXpath;
            IWebElement loginButton;

            if (_webDriver.Title.ToLower().Contains("naukri"))
            {
                Console.WriteLine("Website Loaded Successfully.");
            }

            if(IsElementPresent(GetObj("ID", "emailTxt")))
            {
                 emailFieldElement = GetElement("emailTxt", "ID");

                 passFieldElement = GetElement("pwd1", "ID");

                 loginXpath = "//*[@type='submit' and @value='Login']";
                 loginButton = GetElement(loginXpath, "XPATH");


            }
            else if(IsElementPresent(GetObj("ID", "usernameField")))
            {
                 emailFieldElement = GetElement("usernameField", "ID");

                 passFieldElement = GetElement("passwordField", "ID");

                 loginXpath = "//*[@type='submit']";
                 loginButton = GetElement(loginXpath, "XPATH");
            }
            else
            {
                Console.WriteLine("None of the elements found to login.");

            }

        }

        private bool IsElementPresent(By by)
        {
            try
            {
                _webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }















    }
}
