using Common.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ProjectSlenium.Pages
{
    public class ProjectPage
    {
        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;

        // --- Locators ---
        private readonly By _projectLink = By.XPath("//a[normalize-space()='Projects']"); // Điều chỉnh XPath nếu cần
        private readonly By _titleProject = By.XPath("//h5[contains(text(),'List All')]"); 
        private readonly By _addNewButton = By.XPath("//a[normalize-space()='Add New']");
        private readonly By _titleInput = By.XPath("//input[@placeholder='Title']"); // Điều chỉnh nếu tên placeholder khác
        private readonly By _clientDropdown = By.XPath("//span[contains(@class, 'selection') and contains(text(), 'Client')]");
        private readonly By _clientSearchInput = By.XPath("(//input[@role='searchbox'])[2]");
        private readonly By _estimateInput = By.XPath("//input[@placeholder='Estimated Hour']");
        private readonly By _priorityDropdown = By.XPath("(//span[@role='combobox'])[2]");
        private readonly By _prioritySearchInput = By.XPath("(//input[@role='searchbox'])[2]");
        private readonly By _startDateInput = By.XPath("//input[@name='start_date']");
        private readonly By _startDateSelect = By.XPath("(//a[normalize-space()='18'])[1]");
        private readonly By _dateOkButton = By.ClassName("dtp-btn-ok");
        private readonly By _endDateInput = By.XPath("//input[@name='end_date']");
        private readonly By _endDateOkButton = By.XPath("(//button[@class='dtp-btn-ok btn btn-flat btn-primary btn-sm'][normalize-space()='OK'])[2]");
        private readonly By _summaryInput = By.XPath("//textarea[@name='summary']");
        private readonly By _submitButton = By.XPath("//button[@type='submit']");
        private readonly By _successToast = By.XPath("//div[@class='toast toast-success']");
        private readonly By _searchProjectIn = By.XPath("//input[@class='form-control form-control-sm']");
        private readonly By _projectRow = By.XPath("(//tr[@class='odd'])[1]");
        // --- Elements ---
        private IWebElement ProjectLink => _driver.FindElement(_projectLink);
        private IWebElement TitleProject => _driver.FindElement(_titleProject);
        private IWebElement AddNewButton => _driver.FindElement(_addNewButton);
        private IWebElement TitleInput => _driver.FindElement(_titleInput);
        private IWebElement ClientDropdown => _driver.FindElement(_clientDropdown);
        private IWebElement ClientSearchInput => _driver.FindElement(_clientSearchInput);
        private IWebElement EstimateInput => _driver.FindElement(_estimateInput);
        private IWebElement PriorityDropdown => _driver.FindElement(_priorityDropdown);
        private IWebElement PrioritySearchInput => _driver.FindElement(_prioritySearchInput);
        private IWebElement StartDateInput => _driver.FindElement(_startDateInput);
        private IWebElement StartDateSelect => _driver.FindElement(_startDateSelect);
        private IWebElement DateOkButton => _driver.FindElement(_dateOkButton);
        private IWebElement EndDateInput => _driver.FindElement(_endDateInput);
        private IWebElement EndDateOkButton => _driver.FindElement(_endDateOkButton);
        private IWebElement SummaryInput => _driver.FindElement(_summaryInput);
        private IWebElement SubmitButton => _driver.FindElement(_submitButton);
        private IWebElement SuccessToast => _driver.FindElement(_successToast);
        private IWebElement InSearchProject => _driver.FindElement(_searchProjectIn);
        private IWebElement RowProject => _driver.FindElement(_projectRow);
        // Constructor
        public ProjectPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

       

        // Verify if Project page is displayed
        public bool IsProjectPageDisplayed()
        {
            try
            {
                _wait.Until(d => TitleProject.Displayed);
                return TitleProject.Text.Contains("Projects");
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

        // Add a new project
        public void AddNewProject(string title, string priority, string summary)
        {
            _wait.Until(d => AddNewButton.Displayed);
            _validateHelper.ClickElement(AddNewButton);

            _wait.Until(d => TitleInput.Displayed);
            _validateHelper.SetText(TitleInput, title);

            _validateHelper.ClickElement(ClientDropdown);
            _validateHelper.SetText(ClientSearchInput, "W");
            ClientSearchInput.SendKeys(Keys.Enter);

            _validateHelper.SetText(EstimateInput, "1000");

            _validateHelper.ClickElement(PriorityDropdown);
            _validateHelper.SetText(PrioritySearchInput, "No");
            PrioritySearchInput.SendKeys(Keys.Enter);

            _validateHelper.ClickElement(StartDateInput);
            _validateHelper.ClickElement(StartDateSelect);
            _validateHelper.ClickElement(DateOkButton);

            _validateHelper.ClickElement(EndDateInput);
            _validateHelper.ClickElement(EndDateOkButton);

            _validateHelper.SetText(SummaryInput, summary);

            _validateHelper.ScrollAndClickElementJS(SubmitButton);
        }

        public bool SearchNewProject(string searchText)
        {
            try
            {
                _wait.Until(d => InSearchProject.Displayed);
                _validateHelper.SetText(InSearchProject, searchText);
                InSearchProject.SendKeys(Keys.Enter);

                _wait.Until(d => RowProject.Displayed);
                string rowText = RowProject.Text;
                return rowText.Contains(searchText);
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

        // Get success message
        public string GetSuccessMessage()
        {
            _wait.Until(d => SuccessToast.Displayed);
            return SuccessToast.Text;
        }
    }
}