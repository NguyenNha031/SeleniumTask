using Common.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;


namespace ProjectSlenium.Pages
{
    public class TaskPage
    {
        private readonly IWebDriver _driver;
        private readonly ValidateHelper _validateHelper;
        private readonly WebDriverWait _wait;
        // --- Locators ---
        private readonly By _headerPageTextLocator = By.XPath("//h5[contains(text(),'List All')]");
        private readonly By _addNewButton = By.XPath("//a[normalize-space()='Add New']");
        private readonly By _titleIn = By.XPath("//input[@placeholder='Title']");  
        private readonly By _startDateInput = By.XPath("//input[@name='start_date']");
        private readonly By _startDateSelect = By.XPath("(//a[normalize-space()='11'])[1]");
        private readonly By _dateOkButton = By.ClassName("dtp-btn-ok");
        private readonly By _endDateInput = By.XPath("//input[@name='end_date']");
        private readonly By _endDateOkButton = By.XPath("(//button[@class='dtp-btn-ok btn btn-flat btn-primary btn-sm'][normalize-space()='OK'])[2]");
        private readonly By _estimateInput = By.XPath("//input[@placeholder='Estimated Hour']");
        private readonly By _summaryInput = By.XPath("//textarea[@name='summary']");
        private readonly By _projectDropdown = By.XPath("(//span[@role='combobox'])[1]");
        private readonly By _projectSearchInput = By.XPath("(//input[@role='searchbox'])[1]");
        private readonly By _submitButton = By.XPath("//button[@type='submit']");
        private readonly By _searchTaskIn = By.XPath("//input[@class='form-control form-control-sm']");
        private readonly By _taskRow = By.XPath("(//tr[@role='row'])[2]");
        private readonly By _rowBtn = By.XPath("(//button[@type='button'])[1]");
        private readonly By _updateSta = By.XPath("(//a[@href='#'])[10]");
        private readonly By _0Project = By.XPath("(//span[@class='irs-slider single'])[1]");
        private readonly By _taskDetail = By.XPath("(//div[@class='card-header'])[1]");
        private readonly By _updateTaskToast = By.XPath("(//div[@class='toast toast-success'])[1]");

        // --- Elements ---
        private IWebElement BtnAddNew => _wait.Until(d => d.FindElement(_addNewButton));
        private IWebElement InTitle => _wait.Until(d => d.FindElement(_titleIn));
        private IWebElement StartDateInput => _driver.FindElement(_startDateInput);
        private IWebElement StartDateSelect => _driver.FindElement(_startDateSelect);
        private IWebElement DateOkButton => _driver.FindElement(_dateOkButton);
        private IWebElement EndDateInput => _driver.FindElement(_endDateInput);
        private IWebElement EndDateOkButton => _driver.FindElement(_endDateOkButton);
        private IWebElement SummaryInput => _driver.FindElement(_summaryInput);
        private IWebElement EstimateInput => _driver.FindElement(_estimateInput);
        private IWebElement ProjectDropdown => _driver.FindElement(_projectDropdown);
        private IWebElement ProjectSearchInput => _driver.FindElement(_projectSearchInput);
        private IWebElement SubmitButton => _driver.FindElement(_submitButton);
        private IWebElement InSearchTask => _driver.FindElement(_searchTaskIn);
        private IWebElement RowTask => _driver.FindElement(_taskRow);
        private IWebElement BtnRow => _driver.FindElement(_rowBtn);
        private IWebElement StaUpdate => _driver.FindElement(_updateSta);
        private IWebElement Project0 => _driver.FindElement(_0Project);
        // Constructor
        public TaskPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            _validateHelper = new ValidateHelper(driver);
            // Khởi tạo WebDriverWait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
           
            
        }

        public void AddNewTask(String title)
        {
            _wait.Until(d => d.FindElement(_addNewButton).Displayed);
            _validateHelper.ClickElement(BtnAddNew);

            _wait.Until(d => d.FindElement(_titleIn).Displayed);
            _validateHelper.SetText(InTitle, title);
            _validateHelper.ClickElement(StartDateInput);
            _validateHelper.ClickElement(StartDateSelect);
            _validateHelper.ClickElement(DateOkButton);

            _validateHelper.ClickElement(EndDateInput);
            _validateHelper.ClickElement(EndDateOkButton);
            _validateHelper.SetText(EstimateInput, "1000");
            _validateHelper.ClickElement(ProjectDropdown);
            _validateHelper.SetText(ProjectSearchInput, "Test3");
            ProjectSearchInput.SendKeys(Keys.Enter);
            _validateHelper.SetText(SummaryInput, "OpenQA.Selenium.InvalidSelectorException : invalid selector: Unable to locate an element with the xpath expression (//a[normalize-space()='Add New'] because of the following error:\r\nSyntaxError: Failed to execute 'evaluate' on 'Document': The string '(//a[normalize-space()='Add New']' is not a valid XPath expression.");
            _validateHelper.ScrollAndClickElementJS(SubmitButton);
        }
        public bool SearchandCheckTask(String title)
        {
            try
            {
                _wait.Until(d => InSearchTask.Displayed);
                _validateHelper.SetText(InSearchTask, title);
                InSearchTask.SendKeys(Keys.Enter);

                _wait.Until(d => RowTask.Displayed);
                string rowText = RowTask.Text;
                return rowText.Contains(title);
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
        public void ViewTask()
        {
            _validateHelper.HoverElement(RowTask);

            _wait.Until(d => BtnRow.Displayed);
            _validateHelper.ClickElement(BtnRow);
        }

        public void UpdateTask()
        {
            _wait.Until(d => StaUpdate.Displayed);
            _validateHelper.ClickElement(StaUpdate);
            _validateHelper.DragAndDropByOffset(Project0, 100, 0);
            _validateHelper.ClickElement(SubmitButton);
        }
         public bool IsTaskToastDisplayed()
        {
            try
            {

                _wait.Until(d => d.FindElement(_updateTaskToast).Displayed);
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
        public bool IsTaskDetailDisplayed()
        {
            try
            {

                _wait.Until(d => d.FindElement(_taskDetail).Displayed);
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
