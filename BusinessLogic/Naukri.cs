using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace BusinessLogic
{
    public class Naukri
    {
        private IWebDriver _webDriver;

        public string originalResumePath
        { get; set; } = ConfigurationManager.AppSettings["OriginalResumePath"];

        public string modifiedResumePath { get; set; } = ConfigurationManager.AppSettings["ModifiedResumePath"];

        public string UserName { get; set; } = ConfigurationManager.AppSettings["UserName"];

        public string Password { get; set; } = ConfigurationManager.AppSettings["Password"];

        public string MobileNumber { get; set; } = ConfigurationManager.AppSettings["MobileNumber"];
        public string jobkeysearch { get; set; } = ConfigurationManager.AppSettings["jobkeysearch"];
        public string joblocation { get; set; } = ConfigurationManager.AppSettings["joblocation"];

        public bool UpdatePDF { get; set; } = true;

        public string NaukriURL { get; set; } = @"https://login.naukri.com/nLogin/Login.php";

        public Naukri()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--disable-notifications");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popups");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-dev-shm-usage");
            //options.AddArgument("headless");

            //Disable webdriver flags or you will be easily detectable
            options.AddArgument("--disable-blink-features");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver(options);

            _webDriver.Navigate().GoToUrl(NaukriURL);

            SetImplicitWait(10);

            Login();
        }

        public void TearDown()
        {
            try
            {
                _webDriver.Close();
                Console.WriteLine("Driver Closed Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }

            try
            {
                _webDriver.Quit();

                Console.WriteLine("Driver Quit Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }
        }

        private void SetImplicitWait(int time)
        {
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
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
                return _webDriver.FindElement(_by);
            }

            return null;
        }

        public bool Login()
        {
            bool Status = false;
            IWebElement emailFieldElement = null;
            IWebElement passFieldElement = null;
            String loginXpath;
            IWebElement loginButton = null;

            if (_webDriver.Title.ToLower().Contains("naukri"))
            {
                Console.WriteLine("Website Loaded Successfully.");
            }

            if (IsElementPresent(GetObj("ID", "emailTxt")))
            {
                emailFieldElement = GetElement("emailTxt", "ID");

                passFieldElement = GetElement("pwd1", "ID");

                loginXpath = "//*[@type='submit' and @value='Login']";
                loginButton = GetElement(loginXpath, "XPATH");
            }
            else if (IsElementPresent(GetObj("ID", "usernameField")))
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

            if (emailFieldElement != null)
            {
                emailFieldElement.Clear();
                emailFieldElement.SendKeys(UserName);

                passFieldElement.Clear();
                passFieldElement.SendKeys(Password);

                loginButton.SendKeys(Keys.Enter);
                Thread.Sleep(3000);
                Console.WriteLine("Checking Skip button");
                var skipAdXpath = "//*[text() = 'SKIP AND CONTINUE']";

                if (WaitTillElementPresent(skipAdXpath, "XPATH", 10))
                {
                    GetElement(skipAdXpath, "XPATH").Click();
                }

                if (WaitTillElementPresent("search-jobs", "ID", 40))
                {
                    var CheckPoint = GetElement("search-jobs", "ID");

                    if (CheckPoint != null)
                    {
                        Console.WriteLine("Naukri Login Successful");
                        Status = true;
                        return Status;
                    }
                    else
                    {
                        Console.WriteLine("Unknown Login Error");
                        return Status;
                    }
                }
                else
                {
                    Console.WriteLine("Unknown Login Error");
                    return Status;
                }
            }
            return Status;
        }

        public void UpdateProfile()
        {
            try
            {
                var mobXpath = "//*[@name='mobile'] | //*[@id='mob_number']";
                var profeditXpath = "//a[contains(text(), 'UPDATE PROFILE')] | //a[contains(text(), ' Snapshot')] | //a[contains(@href, 'profile') and contains(@href, 'home')]";
                var saveXpath = "//button[@ type='submit'][@value='Save Changes'] | //*[@id='saveBasicDetailsBtn']";
                var editXpath = "//em[text()='Edit']";

                WaitTillElementPresent(profeditXpath, "XPATH", 20);
                IWebElement profElement = GetElement(profeditXpath, "XPATH");
                profElement.Click();
                SetImplicitWait(2);

                WaitTillElementPresent(editXpath + " | " + saveXpath, "XPATH", 20);

                if (IsElementPresent(GetObj("XPATH", editXpath)))
                {
                    var editElement = GetElement(editXpath, "XPATH");
                    editElement.Click();

                    WaitTillElementPresent(mobXpath, "XPATH", 20);
                    var mobFieldElement = GetElement(mobXpath, "XPATH");
                    mobFieldElement.Clear();
                    mobFieldElement.SendKeys(MobileNumber);
                    SetImplicitWait(2);

                    var saveFieldElement = GetElement(saveXpath, "XPATH");
                    saveFieldElement.SendKeys(Keys.Enter);
                    SetImplicitWait(3);

                    WaitTillElementPresent("//*[text()='today']", "XPATH", 10);
                    if (IsElementPresent(GetObj("XPATH", "//*[text()='today']")))
                    {
                        Console.WriteLine("Profile Update Successful");
                    }
                    else
                    {
                        Console.WriteLine("Profile Update Failed");
                    }
                }
                else if (IsElementPresent(GetObj("XPATH", saveXpath)))
                {
                    var mobFieldElement = GetElement(mobXpath, "XPATH");
                    mobFieldElement.Clear();
                    mobFieldElement.SendKeys(MobileNumber);
                    SetImplicitWait(2);

                    var saveFieldElement = GetElement(saveXpath, "XPATH");
                    saveFieldElement.SendKeys(Keys.Enter);
                    SetImplicitWait(3);

                    WaitTillElementPresent("confirmMessage", "ID", 10);

                    if (IsElementPresent(GetObj("ID", "confirmMessage")))
                    {
                        Console.WriteLine("Profile Update Successful");
                    }
                    else
                    {
                        Console.WriteLine("Profile Update Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }
        }

        public bool WaitTillElementPresent(string elementTag, string locator = "ID", int timeout = 30)
        {
            bool result = false;
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            for (int i = 0; i < timeout; i++)
            {
                Thread.Sleep(990);

                try
                {
                    if (IsElementPresent(GetObj(locator, elementTag)))
                    {
                        result = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception when WaitTillElementPresent::" + ex.Message);
                }
            }

            if (result == false)
            {
                Console.WriteLine("Element not found with");
            }

            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            return result;
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

        public void ApplyForJobs()
        {
            try
            {
                using (DataAccessLayer.DataAccessContext dataAccessContext = new DataAccessLayer.DataAccessContext())
                {
                    var Jobs = dataAccessContext.NaukriJobDetails.Where(x => x.AppliedStatus == false).ToList();

                    foreach (var item in Jobs)
                    {
                        try
                        {
                            string JobURL = item.URL;
                            Console.WriteLine(JobURL);
                            _webDriver.Navigate().GoToUrl(JobURL);
                            Thread.Sleep(2);
                            _webDriver.FindElement(By.CssSelector(".apply-button-container > .waves-ripple")).Click();

                            Thread.Sleep(5000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error::{ ex.Message }");
                        }

                        item.AppliedStatus = true;
                        item.AppliedDate = DateTime.Now;
                    }

                    dataAccessContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error::{ ex.Message }");
            }
        }

        public void UploadResume(string resumePath)
        {
            try
            {
                var attachCVID = "attachCV";
                var CheckPointXpath = "//*[contains(@class, 'updateOn')]";
                var saveXpath = "//button[@type='button']";

                _webDriver.Navigate().GoToUrl("https://www.naukri.com/mnjuser/profile");

                WaitTillElementPresent(attachCVID, "ID", 10);
                var AttachElement = GetElement(attachCVID, "ID");
                AttachElement.SendKeys(resumePath);

                if (WaitTillElementPresent(saveXpath, "ID", 5))
                {
                    var saveElement = GetElement(saveXpath, "XPATH");
                    saveElement.Click();
                }

                WaitTillElementPresent(CheckPointXpath, "XPATH", 30);
                var CheckPoint = GetElement(CheckPointXpath, "XPATH");

                if (CheckPoint != null)
                {
                    var LastUpdatedDate = CheckPoint.Text;

                    Console.WriteLine($"Resume Document Upload Successful.Last Updated date {LastUpdatedDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error::" + ex.Message);
            }
        }
    }
}