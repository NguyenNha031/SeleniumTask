using NUnit.Framework;
using ProjectSlenium.Common; 
using System;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace ProjectSlenium.TestCase
{

    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    [TestFixture]
    public class LoginTest : BaseTest
    {
        [Test]
        [Category("Login")]
        [Description("Login tuần tự với nhiều tài khoản")]
        public void LoginMultipleAccounts_FromExcel()
        {
            string filePath = @"D:\Code-main\ProjectSlenium\TestData\dataTest.xlsx";
            var credentials = ExcelReader.ReadLoginData(filePath);

            foreach (var (email, password) in credentials)
            {
                test.Info($"➡️ Đang thử đăng nhập với: {email} / {password}");

                loginPage.GoToLoginPage(); 
                loginPage.SignIn(email, password);
                Thread.Sleep(1000);

                if (loginPage.IsLoginSuccessful())
                {
                    test.Pass("✅ Đăng nhập thành công.");
                    loginPage.Signout(); 
                    test.Info("🔁 Đã đăng xuất để tiếp tục bộ tiếp theo.");
                }
                else
                {
                    test.Warning("❌ Đăng nhập thất bại, sẽ thử với bộ tiếp theo.");
                    loginPage.ClearLoginForm();
                }
            }
        }
    }

}