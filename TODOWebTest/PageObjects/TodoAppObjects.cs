using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using TODORestfulAPITest.PageObjects;

namespace TODOWebTest.PageObjects
{
    public class TodoAppObjects : PageObjectBase
    {
        public IWebElement footer => WaitForElement(By.ClassName("footer"));
        public IWebElement InputBox => WaitForElement(By.ClassName("new-todo"));
        public IWebElement TodoList => WaitForElement(By.ClassName("todo-list"));
        public IWebElement ToggleAll => WaitForElement(By.XPath("//label[@for='toggle-all']"));
        public IWebElement EditingTask => WaitForElement(By.XPath("//li[@class='editing']"));
        public IWebElement CompletedTask => WaitForElement(By.XPath("//li[@class='completed']"));
        public IReadOnlyCollection<IWebElement> Tasks => TodoList.FindElements(By.TagName("li"));
        public IWebElement ClearCompleted => WaitForElement(By.ClassName("clear-completed"));//todo-count, all-filter, active-filter, completed-filter
        public bool isElementPresent(By locatorKey)
        {
            try
            {
                _driver.FindElement(locatorKey);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }
        public TodoAppObjects(IWebDriver _driver) : base(_driver)
        {

        }
    }
}
