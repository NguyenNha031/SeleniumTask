using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
namespace Common.Helpers
{
    public class ValidateHelper
    {
        private readonly IWebDriver _driver; // Đối tượng WebDriver để điều khiển trình duyệt
        public static WebDriverWait Wait { get; private set; }
        private readonly IJavaScriptExecutor _js; 
        private readonly Actions _action; 
        private readonly int _timeoutWaitForPageLoaded = 10; 
        private SelectElement _select;

        // Constructor: Khởi tạo ValidateHelper với WebDriver
        public ValidateHelper(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _js = (IJavaScriptExecutor)driver;
            _action = new Actions(_driver);
        }

        // Hàm: GetPageTitle
        // Chức năng: Lấy tiêu đề của trang web hiện tại
        public string GetPageTitle()
        {
            WaitForPageLoaded();
            return _driver.Title;
        }
        // Hàm: SetText
        // Chức năng: Xóa nội dung hiện tại của một phần tử và nhập văn bản mới vào
        public void SetText(IWebElement element, string text)
        {
            Wait.Until(d => element.Displayed && element.Enabled);
            element.Clear();
            element.SendKeys(text);
        }

        // Hàm: ClickElement
        // Chức năng: Nhấp chuột trái vào một phần tử
        public void ClickElement(IWebElement element)
        {
            Wait.Until(d => element.Displayed && element.Enabled);
            element.Click();
        }

        // Hàm: ScrollAndClickElementJS
        // Chức năng: Cuộn trang đến phần tử và nhấp vào nó bằng JavaScript
        public void ScrollAndClickElementJS(IWebElement element)
        {
            WaitForPageLoaded();
            Wait.Until(d => element.Displayed && element.Enabled);
            _js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            _js.ExecuteScript("arguments[0].click();", element);
        }

        // Hàm: RightClickElement
        // Chức năng: Nhấp chuột phải vào một phần tử
        public void RightClickElement(IWebElement element)
        {
            Wait.Until(d => element.Displayed && element.Enabled);
            _action.ContextClick(element).Perform();
        }

        // Hàm: VerifyPageTitle
        // Chức năng: Kiểm tra tiêu đề trang có khớp với chuỗi được cung cấp không
        public bool VerifyPageTitle(string title)
        {
            WaitForPageLoaded();
            return _driver.Title.Equals(title);
        }

        // Hàm: VerifyUrl
        // Chức năng: Kiểm tra URL hiện tại có chứa một phần cụ thể của URL không
        public bool VerifyUrl(string urlPart)
        {
            string currentUrl = _driver.Url;
            Console.WriteLine($"Current: {currentUrl}");
            Console.WriteLine($"Expected to contain: {urlPart}");
            return currentUrl.Contains(urlPart);
        }

        // Hàm: VerifyElementText
        // Chức năng: Kiểm tra văn bản của một phần tử có khớp với văn bản được cung cấp không
        public bool VerifyElementText(By element, string text)
        {
            Wait.Until(d => d.FindElement(element).Displayed);
            return _driver.FindElement(element).Text.Equals(text);
        }

        // Hàm: VerifyElementExists
        // Chức năng: Kiểm tra xem một phần tử có tồn tại trên trang hay không
        public bool VerifyElementExists(By element)
        {
            Wait.Until(d => d.FindElement(element).Displayed);
            int size = _driver.FindElements(element).Count;
            return size > 0;
        }

        // Hàm: WaitForPageLoaded
        // Chức năng: Đợi cho đến khi trang được tải hoàn toàn (dựa trên trạng thái document.readyState và jQuery.active)
        public void WaitForPageLoaded()
        {
            Func<IWebDriver, bool> jQueryLoad = driver =>
            {
                try
                {
                    return (long)_js.ExecuteScript("return jQuery.active") == 0;
                }
                catch (Exception)
                {
                    return true;
                }
            };

            Func<IWebDriver, bool> jsLoad = driver =>
            {
                return _js.ExecuteScript("return document.readyState").ToString().Equals("complete");
            };

            try
            {
                WebDriverWait jsWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeoutWaitForPageLoaded));
                jsWait.Until(jsLoad);
                jsWait.Until(jQueryLoad);
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Quá thời gian load trang.");
            }
        }
        public void HoverElementWithJS(IWebElement element)
        {
            Wait.Until(d => element.Displayed && element.Enabled); 
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].dispatchEvent(new Event('mouseover'));", element);
        }
        public void HoverElement(IWebElement element)
        {
            Wait.Until(d => element.Displayed && element.Enabled);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Perform();
        }

        // Hàm: SelectOptionByText
        // Chức năng: Chọn một tùy chọn trong dropdown dựa trên văn bản hiển thị
        public void SelectOptionByText(By element, string text)
        {
            WaitForPageLoaded();
            _select = new SelectElement(_driver.FindElement(element));
            Wait.Until(d => _driver.FindElement(element).Displayed && _driver.FindElement(element).Enabled);
            _select.SelectByText(text);
        }

        // Hàm: SelectOptionByValue
        // Chức năng: Chọn một tùy chọn trong dropdown dựa trên giá trị (value) của nó
        public void SelectOptionByValue(By element, string value)
        {
            WaitForPageLoaded();
            _select = new SelectElement(_driver.FindElement(element));
            Wait.Until(d => _driver.FindElement(element).Displayed && _driver.FindElement(element).Enabled);
            _select.SelectByValue(value);
        }
        public void DragAndDrop(IWebElement sourceElement, IWebElement targetElement)
        {
            Wait.Until(d => sourceElement.Displayed && sourceElement.Enabled);
            Wait.Until(d => targetElement.Displayed && targetElement.Enabled);
            _action.DragAndDrop(sourceElement, targetElement).Perform();
        }
        public void DragAndDropByOffset(IWebElement sourceElement, int xOffset, int yOffset)
        {
            Wait.Until(d => sourceElement.Displayed && sourceElement.Enabled);
            _action.ClickAndHold(sourceElement)
                   .MoveByOffset(xOffset, yOffset)
                   .Release()
                   .Perform();
        }
        // Hàm: SetTextByJS
        // Chức năng: Xóa nội dung và nhập văn bản mới vào một phần tử bằng JavaScript
        public void SetTextByJS(IWebElement element, string text)
        {
            Wait.Until(d => element.Displayed && element.Enabled);
            _js.ExecuteScript("arguments[0].value = arguments[1];", element, text);
        }
        // Hàm: SwitchToIframe (Overload)
        // Chức năng: Chuyển context của WebDriver sang một iframe bằng IWebElement
        public void SwitchToIframe(IWebElement iframeElement)
        {
            Wait.Until(d => iframeElement.Displayed && iframeElement.Enabled);
            _driver.SwitchTo().Frame(iframeElement);
        }

    }
}