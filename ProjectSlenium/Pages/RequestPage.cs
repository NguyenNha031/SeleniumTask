using Common.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace ProjectSlenium.Pages
{
    public class RequestPage
    {
        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;

        // --- Locators ---
        private readonly By _headerPageText = By.XPath("//h5[contains(text(),'List All')]");
        private readonly By _addnewtBtn = By.XPath("//a[normalize-space()='Create Ticket']");
        private readonly By _leaveTypeBtn = By.XPath("//a[@href='https://hrm.anhtester.com/erp/leave-type']");
        private readonly By _leaveTypeIn = By.XPath("//input[@placeholder='Leave Type']");
        private readonly By _dateIn = By.XPath("//input[@placeholder='Days per year']");
        private readonly By _priorityDropdown = By.XPath("//span[@role='combobox']");
        private readonly By _prioritySearchInput = By.XPath("//input[@role='searchbox']");
        private readonly By _headerLeaveType = By.XPath("//div[@class='card']//span[@class='card-header-title mr-2']");
        private readonly By _succesToast = By.XPath(" //div[@class='toast-message']");
        private readonly By _saveBtn = By.XPath("(//button[@type='submit'])[1]");
        private readonly By _searcgIn = By.XPath("//input[@type='search']");
        private readonly By _rowSearch1 = By.XPath("(//tr[@class='odd'])[1]");
        private readonly By _deleteBtn = By.XPath("(//button[@class='btn icon-btn btn-sm btn-light-danger waves-effect waves-light delete'])[1]");
        private readonly By _submitBtn = By.XPath("(//button[@type='submit'])[2]");
        private readonly By _deletedToast = By.XPath("(//div[@class='toast-message'])[1]");
        // --- Elements ---
        private IWebElement BtnAddNew => _driver.FindElement(_addnewtBtn);
        private IWebElement BtnLeaveType => _driver.FindElement(_leaveTypeBtn);
        private IWebElement InLeaveType => _driver.FindElement(_leaveTypeIn);
        private IWebElement InDate => _driver.FindElement(_dateIn);
        private IWebElement PriorityDropdown => _driver.FindElement(_priorityDropdown);
        private IWebElement PrioritySearchInput => _driver.FindElement(_prioritySearchInput);
        private IWebElement ToastSucces => _driver.FindElement(_succesToast);   
        private IWebElement BtnSave => _driver.FindElement(_saveBtn);
        private IWebElement InSearch => _driver.FindElement(_searcgIn);
        private IWebElement SearchRow1 => _driver.FindElement(_rowSearch1);
        private IWebElement BtnDelete => _driver.FindElement(_deleteBtn);
        private IWebElement BtnSubmit => _driver.FindElement(_submitBtn);
        public RequestPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            // Khởi tạo WebDriverWait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }


        public void ViewLeaveTypeAndDelete()
        {
            _validateHelper.SetText(InSearch, "Ho");
            _wait.Until(d => d.FindElement(_rowSearch1).Displayed);
            _validateHelper.HoverElement(SearchRow1);
            _validateHelper.ClickElement(BtnDelete);
            _validateHelper.ClickElement(BtnSubmit);
        }

        public void AddNewLeaveType()
        {
            _wait.Until(d => d.FindElement(_leaveTypeBtn).Displayed);
            _validateHelper.ClickElement(BtnLeaveType);
            _validateHelper.SetText(InLeaveType, "Holiday");
            _validateHelper.SetText(InDate, "7");
            _validateHelper.ClickElement(PriorityDropdown);
            _validateHelper.SetText(PrioritySearchInput, "Y");
            PrioritySearchInput.SendKeys(Keys.Enter);
            _validateHelper.ClickElement(BtnSave);
        }
        public bool IsDeletedToastDisplayed()
        {
            try
            {
                _wait.Until(d => d.FindElement(_deletedToast).Displayed);
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
        public bool IsRowDisplayed()
        {
            try
            {
                _wait.Until(d => d.FindElement(_rowSearch1).Displayed);
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
        public bool IsToastSuccesDisplayed()
        {
            try
            {
                _wait.Until(d => d.FindElement(_succesToast).Displayed);
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
        public bool IsLeaveTypeDisplayed()
        {
            try
            {
                _wait.Until(d => d.FindElement(_headerLeaveType).Displayed);
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

        public bool IsHelpDeskDisplayed()
        {
            try
            {
                _wait.Until(d => d.FindElement(_headerPageText).Displayed);
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
