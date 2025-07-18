using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace SimpleAppium.Drivers
{
    public class DriverFactory
    {
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                var options = new EdgeOptions();
                options.AddArgument("start-maximized");

                driver = new EdgeDriver(options);
            }

            return driver;
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
