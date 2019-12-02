using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MoreLinq;
using NUnit.Framework.Constraints;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FinScraper.AU.Scrapers
{
    public class StGeorge : IScraper
    {
        private ChromeDriver _driver;
        public ScrapeResponse Scrape(ScrapeOptions scrapeOptions)
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", @"D:\var\downloads");
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                

            /*

            var driver = new ChromeDriver(@"D:\var\apple\consensus", chromeOptions);
            foreach (var t in download)
            {
                t.SendKeys(Keys.Enter);

            }
            
        */




            StartAt("https://bbo.stgeorge.com.au/");

            // login
            UsernameInput.SendKeys("");
            PasswordInput.SendKeys("");
            LogonButton.Click();

            // click() doesn't work so we'll send a SPACE to check it
            _driver.FindElementByXPath("//input[@id='accept-terms']").SendKeys(Keys.Space); 

            // click continue
            _driver.FindElementById("continue").Click();

            // find the accounts list and click in the first row (any row would do) and the link and click it
            _driver.FindElementByXPath("//*[@id='accounts-container']/div/table/tbody/tr/th/a").Click();

            // click on "Find older transactions"
            _driver.FindElementByXPath("//button[contains(text(), 'Find older transactions')]").Click();

            // clicking the label beside the Download radio button (which we couldn't click)
            _driver.FindElementByXPath("//label[@for='opt-txn-hist-download']").Click();

            // clear and type in the fromDate we want
            _driver.FindElementByXPath("//input[@id='fromDate']").Clear();
            _driver.FindElementByXPath("//input[@id='fromDate']").SendKeys(scrapeOptions.FromDate);

            // clear and type in the toDate we want
            _driver.FindElementByXPath("//input[@id='toDate']").Clear();
            _driver.FindElementByXPath("//input[@id='toDate']").SendKeys(scrapeOptions.ToDate);

            // check "Select all the accounts I have access to"
            _driver.FindElementByXPath("//label[@for='select-all-accounts']").SendKeys(Keys.Space);

            // Choose CSV download file format
            _driver.FindElementByXPath("//option[@value='CSV']").Click();

            // click the Download Report button
            _driver.FindElementByXPath("//button[contains(text(), 'Download report')]").Click();

            return new ScrapeResponse("");

        }

        private void StartAt(string url)
        {
            _driver = new ChromeDriver(@"D:\VAR")
            {
                Url = url
            };

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // make the page elements not so squished and affecting each other
            _driver.Manage().Window.Maximize();
            
        }

        private IWebElement UsernameInput => _driver.FindElementById("internet_userid");

        private IWebElement PasswordInput => _driver.FindElementById("internet_pwd");

        private IWebElement LogonButton => _driver.FindElementById("logonBtn");

        private IEnumerable<IWebElement> AcceptTermsCheckBox => _driver.FindElementsById("accept-terms");

        private IWebElement ContinueButton => _driver.FindElementById("continue");


        private WebDriverWait Wait => new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

        private string Url
        {
            set => _driver.Url = value;
        }

        private IEnumerable<IWebElement> AccountLinks => _driver.FindElementsByClassName("account-name");



      //  <a href = "javascript: void(0);" data-bind="attr: { tabindex : accountKeyInfo.accountType == 13 ? -1 : 0 }, text: accountKeyInfo.accountName | fit:50" tabindex="0">ONE MANAGED INVESTMENT FUNDS LIMITED ACF ISG RE...</a>
    }
}
