using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace SimpleAppium.Drivers
{
    public class DriverFactory
    {
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver()
        {
            if (!driver.IsValueCreated || driver.Value == null)
            {
                var options = new EdgeOptions();
                options.AddArgument("start-maximized");
                driver.Value = new EdgeDriver(options);
            }

            return driver.Value;
        }


        public static void QuitDriver()
        {
            if (driver.IsValueCreated && driver.Value != null)
            {
                driver.Value.Quit();
                driver.Dispose();
                driver = new ThreadLocal<IWebDriver>(); 
            }
        }

    }
}
