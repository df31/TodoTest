using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TODORestfulAPITest.PageObjects
{
    public class PageObjectBase
    {
        protected IWebDriver _driver;

        public PageObjectBase(IWebDriver driver)
        {
            _driver = driver;
        }

        private WebDriverWait getWait(int _timeOut = 2)
        {
            return new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeOut));
        }

        public IWebElement WaitForElement(By by, int timeOut = 5)
        {
            var wait = getWait(timeOut);
            var element = wait.Until(d => d.FindElement(by));
            return element;
        }

        public IReadOnlyCollection<IWebElement> WaitForElements(By by, int timeOut = 5)
        {
            var wait = getWait(timeOut);
            var element = wait.Until(d => d.FindElements(by));
            return element;
        }

    }
}
