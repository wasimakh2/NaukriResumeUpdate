﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace BusinessLogic
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

            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("headless");
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
