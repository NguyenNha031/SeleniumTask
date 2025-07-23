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
    public class ProfileTest : BaseTest
    {
        private DashboardPage dashboardPage;
        private ProfilePage profilePage;

        [SetUp]
        public void SetupTaskTest()
        {
            dashboardPage = new DashboardPage(driver);
            profilePage = new ProfilePage(driver);
            LoginAsAdmin();
        }
        [Test, Category("Profile")]
        [Description("Kiểm tra hiển thị các yếu tố chính trên trang Profile")]
        public void ProfileTestCase()
        {
            try
            {

                string firstName = "admin_example";
                string lastName = "hello";
                string phone = "0123321";

                //Thực hiện điều hướng tới trang Profile từ trang Dashboard
                test.Info("Điều hướng đến trang Profile.");
                dashboardPage.ProfileUser();
                test.Pass("✅ Đã mở trang Profile thành công.");

                //Thực hiện điều hướng tới mục Basic Infomation trong Profile và có xác minh
                test.Info("Điều hướng đến  Basic Infomation trong Profile.");
                profilePage.ProfileUser();
                Assert.That(profilePage.IsBasicInfoDisplayed(), Is.True, "Trang Dashboard không hiển thị.");
                test.Pass("✅ Các yếu tố trên Basic Infomation đã hiển thị chính xác.");
                Thread.Sleep(3000);
                test.Pass("✅ Click vào profile người dùng thành công.");

                test.Info("Thực hiện chỉnh sửa mục Basic Infomation trong Profile.");
                profilePage.EditPro5(firstName, lastName, phone);
                Thread.Sleep(3000);
                test.Pass("✅ Đã Update basic infomation thành công.");
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
