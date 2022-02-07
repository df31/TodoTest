using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TODOWebTest
{
    public class WebDriverFactory
    {
        public static IWebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--disable-notifications");
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("--start-maximized");
            options.AddAdditionalOption("useAutomationExtension", false);

            var seleniumDir = @$"{Environment.CurrentDirectory}\..\..\..\..\TODOWebTest\Bin\Debug\netcoreapp3.1";
            return new ChromeDriver(seleniumDir, options);
        }
    }
}
