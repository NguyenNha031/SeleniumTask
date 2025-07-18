using NUnit.Framework;
using ProjectSlenium.Common;
using ProjectSlenium.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesterSetUp.Pages;
using AventStack.ExtentReports;

namespace ProjectSlenium.TestCase
{
    [TestFixture]
    [Explicit]
    public class RequestsTest : BaseTest
    {
        private DashboardPage dashboardPage;
        private ProfilePage profilePage;
        private TaskPage taskPage;
        private RequestPage requestPage;
        [SetUp]
        public void SetupRequestsTest()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            profilePage = new ProfilePage(driver);
            taskPage = new TaskPage(driver);
            requestPage = new RequestPage(driver);
            LoginAsAdmin();
        }


        [Test, Category("Requests")]
        [Description("Thực hiện tạo Requests mới.")]
        public void RequestsTestCase()
        {
            try
            {
                //Thực hiện việc mở trang Requests từ Dashboard, sau đó xác minh
                test.Info("Điều hướng đến trang Requests rồi tới trang Leave Requests.");
                dashboardPage.LeaveRequests();
                Assert.That(requestPage.IsHelpDeskDisplayed(), Is.True, "Trang Leave Requests không hiển thị.");
                test.Pass("✅ Đã hiện được trang Leave Requests.");

                //Thực hiện việc mở trang thêm mới Leave Type rồi xác minh
                test.Info("Thực hiện thêm Leave Type mới.");
                requestPage.AddNewLeaveType();
                Assert.That(requestPage.IsLeaveTypeDisplayed(), Is.True, "Trang Leave Type không hiển thị.");
                test.Pass("✅ Đã hiện được trang Leave Type.");
                Assert.That(requestPage.IsToastSuccesDisplayed(), Is.True, "Không thêm được Leave Type mới.");
                test.Pass("✅ Đã thêm được Leave Type mới.");

                //Thực hiện việc tìm kiếm Leave Type vừa tạo rồi sau đó kiểm tra rồi xóa
                test.Info("Thực hiện tìm kiếm xóa Leave Type mới tạo.");
                requestPage.ViewLeaveTypeAndDelete();
                Assert.That(requestPage.IsRowDisplayed(), Is.True, "Không tìm thấy được Leave Type.");
                test.Pass("✅ Đã tìm thấy được Leave Type.");
                Assert.That(requestPage.IsDeletedToastDisplayed(), Is.True, "Không xóa được Leave Type.");
                test.Pass("✅ Đã xóa được Leave Type.");
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
