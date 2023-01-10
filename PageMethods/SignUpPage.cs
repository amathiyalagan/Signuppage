using ELITSignUpPage.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELITSignUpPage.PageMethods
{
    class SignUpPage: ReportsGenerationClass
    {
        private IWebDriver driver;
        public TestContext TestContext { get; set; }
        private string Instance;

        string signup = "//button[text()='Create Supplier Account']";
        string mouseaction = "/html/body/div/div/span/div/div[2]/div/div[3]";
        string accept = "//span[text()='I Agree']";
        string decline = "//span[text()='I Decline']";
        string companyname = "//label[text()='Company Name*']//parent::div/div/input";
        string goback = "//span[text()='Go back to Login']";

        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        [SetUp]
        public void GoToPage()
        {
            Instance = TestContext.Parameters["inst"];
            driver.Navigate().GoToUrl(Instance);
            TestContext.Progress.WriteLine(Instance + "---Instance Opened Successfully---");
        }

        public void Clickon_Signup()
        {
            Thread.Sleep(1500);
            driver.FindElement(By.XPath(signup)).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(mouseaction)));
        }

        public void Mouseaction()
        {
            Thread.Sleep(1500);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(mouseaction)));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            Thread.Sleep(1000);
        }

        public void Accept()
        {
            driver.FindElement(By.XPath(accept)).Click();
            Thread.Sleep(1000);
        }

        public Boolean VerifyAccept()
        {
            bool res = driver.FindElement(By.XPath(companyname)).Displayed;
            return res;
        }

        public void LoginPage()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(goback)).Click();
            Thread.Sleep(1000);
        }

        public void Decline()
        {
            driver.FindElement(By.XPath(decline)).Click();
            Thread.Sleep(1500);
        }

        public Boolean VerifyDecline()
        {
            Thread.Sleep(1000);
            bool res = driver.FindElement(By.XPath(signup)).Displayed;
            return res;
        }

        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}

    

