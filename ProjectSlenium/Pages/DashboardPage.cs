using Common.Helpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSlenium.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;

        // --- Locators ---
        private readonly By _headerPageTextLocator = By.XPath("//a[@class='b-brand']//img[@class='logo logo-lg']");
        private readonly By _pro5Btn= By.XPath("//a[@data-original-title='Account Settings']");
        private readonly By _taskBtn = By.XPath("//a[@href='https://hrm.anhtester.com/erp/tasks-list']");
        private readonly By _projectBtn = By.XPath("//a[@href='https://hrm.anhtester.com/erp/projects-list']");
        private readonly By _requestsBtn = By.XPath("//a[normalize-space()='Requests']");
        private readonly By _leaverqBtn = By.XPath("//a[normalize-space()='Leave Request']");

        // --- Elements ---
        private IWebElement Btnpro5 => _driver.FindElement(_pro5Btn);
        private IWebElement HeaderPageText => _driver.FindElement(_headerPageTextLocator);
        private IWebElement BtnTask => _driver.FindElement(_taskBtn);
        private IWebElement Btnproject => _driver.FindElement(_projectBtn);
        private IWebElement BtnRequests => _driver.FindElement(_requestsBtn);
        private IWebElement BtnLeaveRq => _driver.FindElement(_leaverqBtn);

        // Constructor
        public DashboardPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            // Khởi tạo WebDriverWait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void ProfileUser()
        {
            _wait.Until(d => d.FindElement(_pro5Btn).Displayed);
            _validateHelper.ClickElement(Btnpro5);

        }
        public void TaskUser()
        {
            _wait.Until(d => d.FindElement(_taskBtn).Displayed);
            _validateHelper.ClickElement(BtnTask);

        }
        public void ProjectUser()
        {
            _wait.Until(d => d.FindElement(_taskBtn).Displayed);
            _validateHelper.ClickElement(Btnproject);
        }
        public void LeaveRequests()
        {
            _wait.Until(d => d.FindElement(_requestsBtn).Displayed);
            _validateHelper.ClickElement(BtnRequests);
            _wait.Until(d => d.FindElement(_leaverqBtn).Displayed);
            _validateHelper.ClickElement(BtnLeaveRq);
        }
        public bool IsDashboardPageDisplayed()
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
