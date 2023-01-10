using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELITSignUpPage.Config
{
    class ReportsGenerationClass
    {
        protected ExtentReports _extent;
        protected ExtentTest _test;
        public IWebDriver _driver;

        [OneTimeSetUp]
        protected void Setup()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;  //Getting path of Project Assembly
            var actualPath = path.Substring(0, path.LastIndexOf("bin")); // Removing Bin from the path
            var projectPath = new Uri(actualPath).LocalPath;  //Converting actualpath into Uri and assigning to projectpath
            Directory.CreateDirectory(projectPath.ToString() + "Reports");  //Creating new folder
            DateTime time = DateTime.Now;
            String fileName = this.GetType().Name;
            var reportPath = projectPath + "Reports\\" + fileName + "--" + time.ToString("dd_MMM_yyyy_hh_mm_ss") + ".html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);  //Initiating aventstack's extenthtml reporter to create HTML reports and passing file path to create HTML 
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);  //
            _extent.AddSystemInfo("Host Name", "ELIT");
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("UserName", "ArunKumar");
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            _extent.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false); // Disabling header which contains 'Chrome is running by Automated Software'
            options.AddUserProfilePreference("credentials_enable_service", false);  //disabling save password Popup
            options.AddUserProfilePreference("profile.password_manager_enabled", false);  // Diabling Update password Popup
            options.AddArguments("start-maximized");  //starting chrome in Maximised Window

            _driver = new ChromeDriver(@"C:\Users\ATSTC004\Desktop\repos\ELIT_Selenium_PRJ\ELIT_Selenium_PRJ\Driver", options);  //Location of Chrome Driver.EXE in the project
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20); //Implicit Wait Timeout
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name); //Creating TestName for Reports
        }

        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;  //Getting status of Test
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);  //Getting stack trace of test
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = this.GetType().Name + "-" + time.ToString("dd_MMM_yyyy_hh_mm_ss") + ".png";
                    //String fileName = _test+time.ToString("dd_MM_yyyy_hh_mm_ss") + ".png";
                    String screenShotPath = Capture(_driver, fileName);
                    _test.Log(Status.Fail, "Fail");
                    _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _extent.Flush();  // Erases any previous data on a relevant report and creates a whole new report
            _driver.Quit();  // Closes the chrome sessions
        }

        public IWebDriver GetDriver()
        {
            return _driver;
        }

        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }
    }
}

    

