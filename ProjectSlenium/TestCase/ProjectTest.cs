using NUnit.Framework;
using ProjectSlenium.Common;
using ProjectSlenium.Pages;
using System;
using System.Threading;
using AventStack.ExtentReports;
using TesterSetUp.Pages;
using System.Windows.Forms;

namespace ProjectSlenium.TestCase
{
    [TestFixture]
    [Explicit]
    public class ProjectTest : BaseTest
    {
        private ProjectPage projectPage;
        private DashboardPage dashboardPage;

        [SetUp]
        public void SetupTaskTest()
        {
            dashboardPage = new DashboardPage(driver);
            projectPage = new ProjectPage(driver);
            LoginAsAdmin();
        }

        [Test, Category("Project")]
        [Description("Kiểm tra thêm mới dự án trên trang Project")]
        public void ProjectTestCase()
        {
            try
            {
                string title = "Đây là project Test4";
                string priority = "Normal";
                string summary = "Test Project 112312323";

                //Thực hiện việc mở trang Project từ Dashboard, sau đó xác minh 
                test.Info("Điều hướng đến trang Project.");
                dashboardPage.ProjectUser();
                Assert.That(projectPage.IsProjectPageDisplayed(), Is.True, "Trang Project không hiển thị.");
                test.Pass("✅ Trang Project đã hiển thị chính xác.");

                //Thực hiện việc thêm Project mới 
                test.Info("Thực hiện thêm mới dự án.");
                projectPage.AddNewProject(title, priority, summary);
                Thread.Sleep(3000);
                test.Pass("✅ Đã thêm mới dự án thành công.");
                Thread.Sleep(3000);

                //THực hiện tìm kiếm Project sau đó kiểm tra xem dự án vừa tạo có tồn tại không
                test.Info("Thực hiện tìm kiếm dự án.");
                bool isProjectFound = projectPage.SearchNewProject(title);
                Assert.That(isProjectFound, Is.True, $"Không tìm thấy dự án với nội dung '{title}' trong hàng đầu tiên.");
                test.Pass($"✅ Tìm thấy dự án '{title}' trong hàng đầu tiên.");
                Thread.Sleep(2000);
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