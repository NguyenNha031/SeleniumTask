using OpenQA.Selenium;
using Common.Helpers; // Giả sử ValidateHelper nằm ở đây
using System;
using OpenQA.Selenium.Support.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TesterSetUp.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;

        // --- Locators ---
        private readonly By _headerPageTextLocator = By.XPath("//a[@class='b-brand']//img[@class='logo logo-lg']");
        private readonly By _signOutBtn = By.XPath("//a[@class='btn btn-smb btn-outline-primary rounded-pill']");
        private readonly By _emailFieldLocator = By.Id("iusername");
        private readonly By _passwordFieldLocator = By.Id("ipassword");
        private readonly By _loginButtonLocator = By.XPath("//button[@type='submit']");
        private readonly By _errorToast = By.XPath("//div[@class='toast toast-error']");

        // --- Elements ---
        private IWebElement BtnSignout => _driver.FindElement(_signOutBtn);
        private IWebElement EmailField => _driver.FindElement(_emailFieldLocator);
        private IWebElement PasswordField => _driver.FindElement(_passwordFieldLocator);
        private IWebElement LoginButton => _driver.FindElement(_loginButtonLocator);
       
        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            // Khởi tạo WebDriverWait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // --- Actions ---
        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl("https://hrm.anhtester.com/erp/login");
        }

        public void SignIn(string email, string password)
        {
            _wait.Until(d => d.FindElement(_emailFieldLocator).Displayed);

            _wait.Until(d => d.FindElement(_emailFieldLocator).Displayed);
            _validateHelper.SetText(EmailField, email);
            //_validateHelper.ClickElement(BtnConti);

            //_wait.Until(d => d.FindElement(_passwordFieldLocator).Displayed);
            _validateHelper.SetText(PasswordField, password);

            _validateHelper.ClickElement(LoginButton);

        }
        public String getTextError()
        {
            try
            {
              
                IWebElement errorToastElement = _wait.Until(d => d.FindElement(_errorToast));

                string errorMessage = errorToastElement.Text;

                return errorMessage;
            }
            catch (WebDriverTimeoutException)
            {
                return "Không tìm thấy thông báo lỗi.";
            }
        }

        public void Signout()
        {
            _wait.Until(d => d.FindElement(_signOutBtn).Displayed);
            _validateHelper.ClickElement(BtnSignout);

        }


        // --- Verifications ---
        public bool VerifySigninPageTitle(string expectedTitle)
        {
            return _wait.Until(d => d.Title.Contains(expectedTitle));
        }

        public bool IsLoginSuccessful()
        {
            try
            {

                _wait.Until(d => d.FindElement(_headerPageTextLocator).Displayed);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
