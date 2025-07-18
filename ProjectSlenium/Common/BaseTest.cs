using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using ProjectSlenium.Helpers;
using ProjectSlenium.Pages;
using SimpleAppium.Drivers;
using System;
using System.IO;
using System.Threading;
using TesterSetUp.Pages;

namespace ProjectSlenium.Common
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected ExtentReports extent;
        protected ExtentTest test;
        protected LoginPage loginPage; 

        [OneTimeSetUp]
        public void SetupReporting()
        {
            extent = ExtentManager.GetExtent();
            Console.WriteLine("✅ OneTimeSetUp: Extent initialized");
        }

        [SetUp]
        public void SetUpTest()
        {
            driver = DriverFactory.GetDriver();
            Console.WriteLine("🧪 Test started: " + TestContext.CurrentContext.Test.Name);
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            loginPage = new LoginPage(driver);
        }
        protected void LoginAsAdmin()
        {

            loginPage = new LoginPage(driver);
            test.Info("Thực hiện đăng nhập để truy cập trang.");
            loginPage.GoToLoginPage();
            loginPage.SignIn("admin_example", "123456");
            Assert.That(loginPage.IsLoginSuccessful(), Is.True, "Đăng nhập thất bại.");
            test.Pass("✅ Đã đăng nhập thành công.");
        }

        [TearDown]
        public void TearDownTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

          
            if (loginPage.IsLoginSuccessful())
            {
                try
                {
                    test.Info("Thực hiện đăng xuất sau khi test hoàn thành.");
                    loginPage.Signout();
                    test.Pass("✅ Đăng xuất thành công.");
                }
                catch (Exception ex)
                {
                    test.Log(Status.Warning, $"⚠️ Lỗi trong quá trình đăng xuất: {ex.Message}");
                }
            }

            DriverFactory.QuitDriver();

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("❌ Test Failed");
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("✅ Test Passed");
            }
        }

        [OneTimeTearDown]
        public void TearDownReporting()
        {
            Console.WriteLine("📤 Flushing report...");
            extent.Flush();
        }

        protected string CaptureScreenshot(string testName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string reportsDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            Directory.CreateDirectory(reportsDir);
            var screenshotPath = Path.Combine(reportsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }
    }
}