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
    public class ProfilePage
    {


        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;

        // --- Locators ---
        private readonly By _basicInfo = By.Id("user-set-basicinfo-tab");
        private readonly By _headerPageTextLocator = By.XPath("//span[@class='p-l-5'][normalize-space()='Basic Information']");
        private readonly By _firstNameIn = By.XPath("//input[@placeholder='First Name']");
        private readonly By _lastNameIn = By.XPath("//input[@placeholder='Last Name']");
        private readonly By _phoneIn = By.XPath("//input[@name='contact_number']");
        private readonly By _genderDD = By.XPath("(//span[@class='selection'])[5]");
        private readonly By _searchIn = By.XPath("//input[@role='searchbox']");
        private readonly By _submitBtn = By.XPath("(//button[@type='submit'])[1]");
        private readonly By _succesToast = By.XPath("//div[@class='toast toast-success']");
        private readonly By _signOutBtn = By.XPath("//a[@class='btn btn-smb btn-outline-primary rounded-pill']");
        // --- Elements ---
        //div[@class='toast-message']
        private IWebElement BtnBasicInfo => _driver.FindElement(_basicInfo);
        private IWebElement HeaderPageText => _driver.FindElement(_headerPageTextLocator);
        private IWebElement InFirstName => _driver.FindElement(_firstNameIn);   
        private IWebElement InLastName => _driver.FindElement(_lastNameIn);
        private IWebElement InPhone => _driver.FindElement(_phoneIn);
        private IWebElement DDgen => _driver.FindElement(_genderDD);
        private IWebElement Insearch => _driver.FindElement(_searchIn);
        private IWebElement Btnsubmit => _driver.FindElement(_submitBtn);
        private IWebElement ToastSucces => _driver.FindElement(_succesToast);
        private IWebElement BtnSignout => _driver.FindElement(_signOutBtn);

        // Constructor
        public ProfilePage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            // Khởi tạo WebDriverWait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }


        public void ProfileUser()
        {
            _wait.Until(d => d.FindElement(_basicInfo).Displayed);

            _validateHelper.ClickElement(BtnBasicInfo);

        }

        public void EditPro5(String firstName, String lastName, String phone)
        {
            _wait.Until(d => d.FindElement(_firstNameIn).Displayed);
            _validateHelper.SetText(InFirstName, firstName);
            _validateHelper.SetText(InLastName, lastName);
            _validateHelper.SetText(InPhone, phone);
            _validateHelper.ClickElement(DDgen);

            _validateHelper.SetText(Insearch, "f");
            Insearch.SendKeys(Keys.Enter);
            _validateHelper.ScrollAndClickElementJS(Btnsubmit);
            _validateHelper.ScrollAndClickElementJS(BtnSignout);


        }
        public bool IsBasicInfoDisplayed()
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

        public string GetSuccessMessage()
        {
            _wait.Until(d => d.FindElement(_succesToast).Displayed);
            IWebElement successToastElement = _driver.FindElement(_succesToast);
            string successMessage = successToastElement.Text;
            return successMessage;
        }
    }
}
