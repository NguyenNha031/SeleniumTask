using NUnit.Framework;
using ProjectSlenium.Common;
using TesterSetUp.Pages;
using System;
using ProjectSlenium.Pages;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using System.Threading;
using System.Windows.Forms;


namespace ProjectSlenium.TestCase
{
    [TestFixture]
    [Explicit]
    public class DashboardTest : BaseTest
    {
        private DashboardPage dashboardPage;
        [SetUp]
        public void SetupTaskTest()
        {
            dashboardPage = new DashboardPage(driver);
            LoginAsAdmin();
        }

        [Test, Category("Dashboard")]
        [Description("Kiểm tra hiển thị các yếu tố chính trên trang Dashboard")]
        public void VerifyDashboardElementsDisplay()
        {
            try
            {
                //Thực hiện kiểm tra có vào được trang Dashboard được không
                test.Info("Kiểm tra trang Dashboard đã được hiển thị.");
                Assert.That(dashboardPage.IsDashboardPageDisplayed(), Is.True, "Trang Dashboard không hiển thị.");
                test.Pass("✅ Các yếu tố trên Dashboard đã hiển thị chính xác.");
                test.Info("Click vào liên kết hồ sơ người dùng trên Dashboard.");
                dashboardPage.ProfileUser();
                Thread.Sleep(3000);
                test.Pass("✅ Click vào profile người dùng thành công.");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                test.AddScreenCaptureFromPath(screenshotPath, "Screenshot on Failure");
                test.Fail($"❌ Test thất bại: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            // Chỉ đăng xuất khi test trước đó đã đăng nhập thành công
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
        }
    }
}