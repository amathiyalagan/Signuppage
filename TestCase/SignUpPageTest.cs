using AventStack.ExtentReports;
using ELITSignUpPage.Config;
using ELITSignUpPage.PageMethods;
using NUnit.Framework;
using System;

namespace ELITSignUpPage.Testcase
{
    [TestFixture]
    class SignUpPageTest:ReportsGenerationClass
    {
        SignUpPage SignUpPage;

        [Test, Order(1)]
        [Category("User Aggreement")]
        public void accepting_user_agreement()
        {
            SignUpPage = new SignUpPage(GetDriver());
            try
            {
                SignUpPage.GoToPage();
                SignUpPage.Clickon_Signup();
                _test.Log(Status.Pass, "Clicked on SignUp");

                SignUpPage.Mouseaction();
                SignUpPage.Accept();
                _test.Log(Status.Pass, "Accepted Terms and Conditions");

                _test.Log(Status.Pass, "Returned back to LOGIN Page : " + SignUpPage.VerifyAccept().ToString());
                Assert.IsTrue(SignUpPage.VerifyAccept());
                SignUpPage.LoginPage();
            }
            catch (Exception ex)
            {
                DateTime time = DateTime.Now;
                String fileName = "Screenshot_" + time.ToString("dd_MM_yyyy_hh_mm_ss") + ".png";
                String screenShotPath = Capture(_driver, fileName);
                _test.Log(Status.Fail, ex.ToString());
                _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
            }
            finally
            {
                SignUpPage.closeBrowser();
            }
        }

        [Test, Order(2)]
        [Category("User Disaggreement")]
        public void declining_user_agreement()
        {
            SignUpPage = new SignUpPage(GetDriver());
            try
            {
                SignUpPage.GoToPage();
                SignUpPage.Clickon_Signup();
                _test.Log(Status.Pass, "Clicked on SignUp");

                SignUpPage.Mouseaction();
                SignUpPage.Decline();
                _test.Log(Status.Pass, "Declined Terms and Conditions");

                _test.Log(Status.Pass, "Returned back to LOGIN Page : " + SignUpPage.VerifyDecline().ToString());
                Assert.IsTrue(SignUpPage.VerifyDecline());
            }
            catch (Exception ex)
            {
                DateTime time = DateTime.Now;
                String fileName = "Screenshot_" + time.ToString("dd_MM_yyyy_hh_mm_ss") + ".png";
                String screenShotPath = Capture(_driver, fileName);
                _test.Log(Status.Fail, ex.ToString());
                _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
            }
            finally
            {
                SignUpPage.closeBrowser();
            }
        }
    }

    
}
