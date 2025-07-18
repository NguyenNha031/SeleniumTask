using NUnit.Framework;
using ProjectSlenium.Common; 
using System;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using System.Threading;

namespace ProjectSlenium.TestCase
{
    [TestFixture]
    public class LoginTest : BaseTest
    {
        [SetUp]
        public void SetupTest()
        {
            test.Info("Điều hướng đến trang đăng nhập");
            loginPage.GoToLoginPage();
        }

        [Test, Order(1)]
        [Category("Login")]
        [Description("Kiểm tra đăng nhập với tài khoản hợp lệ")]
        public void LoginWithValidCredentials()
        {
            try
            {
                string email = "admin_example";
                string password = "123456";
                test.Info($"Thực hiện đăng nhập với tài khoản: {email}");
                loginPage.SignIn(email, password);
                test.Info("Kiểm tra kết quả sau khi đăng nhập");
                Assert.That(loginPage.IsLoginSuccessful(), Is.True, "Đăng nhập thất bại, không tìm thấy yếu tố xác nhận.");
                test.Pass("✅ Đăng nhập thành công với tài khoản hợp lệ.");
            }
            catch (Exception ex)
            {
                test.Fail($"❌ Test thất bại: {ex.Message}");
                throw;
            }
        }

        [Test, Order(2)]
        [Category("Login")]
        [Description("Kiểm tra đăng nhập với mật khẩu không hợp lệ")]
        public void LoginWithInvalidPassword()
        {
            try
            {
                string email = "admin@example.com";
                string invalidPassword = "wrong-password";
                test.Info($"Thực hiện đăng nhập với tài khoản sai: {email}");
                loginPage.SignIn(email, invalidPassword);
                string actualErrorMessage = loginPage.getTextError();
                test.Info($"Thông báo lỗi nhận được: '{actualErrorMessage}'");
                string expectedErrorMessage = "Invalid Login Credentials.";
                Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage), "Nội dung thông báo lỗi không đúng như mong đợi.");
                test.Pass("✅ Kiểm tra đăng nhập với mật khẩu sai đã hiển thị đúng thông báo lỗi.");
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