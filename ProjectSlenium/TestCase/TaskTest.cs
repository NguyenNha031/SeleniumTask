using NUnit.Framework;
using ProjectSlenium.Common;
using ProjectSlenium.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using TesterSetUp.Pages;
using AventStack.ExtentReports;
using Common.Helpers;
namespace ProjectSlenium.TestCase
{
    [TestFixture]
    [Explicit]
    public class TaskTest : BaseTest
    {

        private DashboardPage dashboardPage;
        private ProfilePage profilePage;
        private TaskPage taskPage;
        private ValidateHelper validateHelper;
        [SetUp]
        public void SetupTaskTest()
        {
            dashboardPage = new DashboardPage(driver);
            profilePage = new ProfilePage(driver);
            taskPage = new TaskPage(driver);
            validateHelper = new ValidateHelper(driver);
            LoginAsAdmin();
        }
        [Test, Category("Task")]
        [Description("Thực hiện các test case trong Task.")]
        public void TaskTestCase()
        {
            try
            {
                string title = "Test task 2";
                //Mở Task từ Đashboard -> Xác minh mở thành công
                dashboardPage.TaskUser();
                Assert.That(taskPage.IsBasicInfoDisplayed(), Is.True, "Trang Task không hiển thị.");
                test.Pass("✅ Đã hiện được trang task.");
                //Thực hiện việc thêm Task mới 
                taskPage.AddNewTask(title);
                test.Pass("✅ Đã thêm mới task thành công.");
                Thread.Sleep(3000);

                //Search vào ô tìm kiếm title của Task mới vừa tạo để check tạo thành công không
                test.Info("Thực hiện tìm kiếm task.");
                bool isProjectFound = taskPage.SearchandCheckTask(title);
                Assert.That(isProjectFound, Is.True, $"Không tìm thấy dự án với nội dung '{title}' trong hàng đầu tiên.");
                test.Pass($"✅ Tìm thấy task '{title}' trong hàng đầu tiên.");

                //Thực hiện việc hover vào Task để click vào xem Task detail
                test.Info("Thực hiện hover và click task.");
                taskPage.ViewTask();
                Assert.That(taskPage.IsTaskDetailDisplayed(), Is.True, "Trang Task detail không hiển thị.");
                test.Pass("✅ Đã mở task detail thành công.");
                Thread.Sleep(3000);

                //Thực hiện update Task status và xác minh 
                test.Info("Thực hiện xem và update task.");
                taskPage.UpdateTask();
                Assert.That(taskPage.IsTaskToastDisplayed(), Is.True, "Update task không thành công.");
                test.Pass("✅ Đã Update task thành công.");
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
