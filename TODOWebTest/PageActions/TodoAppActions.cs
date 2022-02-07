using System;
using OpenQA.Selenium;
using System.Collections;
using TODOWebTest.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using Microsoft.Extensions.Configuration;

namespace TODOWebTest.PageActions
{
    public class TodoAppActions : IDisposable
    {
        private IWebDriver _driver;
        private TodoAppObjects _todoAppPage;

        public TodoAppActions(IWebDriver driver, IConfiguration config)
        {
            _driver = driver;
            _driver.Url = config["TodoApp:Url"];
            _todoAppPage = new TodoAppObjects(_driver);
        }
        public void AddTask(string taskName)
        {
            _todoAppPage.InputBox.Clear();
            _todoAppPage.InputBox.SendKeys(taskName);
        }
        public void PressEnter()
        {
            _todoAppPage.InputBox.SendKeys(Keys.Enter);
        }
        public void ClickMarkUnmark()
        {
            _todoAppPage.ToggleAll.Click();
        }
        public void ClickClearCompleted()
        {
            _todoAppPage.ClearCompleted.Click();
        }

        public List<IWebElement> GetAllTasks()
        {
            List<IWebElement> allTasks = new List<IWebElement>();
            if (TodoListIsPresent())
            {
                foreach (var item in _todoAppPage.Tasks)
                {
                    allTasks.Add(item);
                }
            }
            return allTasks;
        }

        public List<Dictionary<string, IWebElement>> GetActiveTasks()
        {
            List<Dictionary<string, IWebElement>> activeTasks = new List<Dictionary<string, IWebElement>>();
            var alltasks = GetAllTasks();
            if (alltasks.Count != 0)
            {
                foreach (var item in alltasks)
                {
                    if (item.GetAttribute("class") == string.Empty)
                    {
                        Dictionary<string, IWebElement> activeTask = new Dictionary<string, IWebElement>();
                        var elements = item.FindElements(By.XPath("div[@class='view']/*"));
                        activeTask.Add("toggle",elements[0]);
                        activeTask.Add("label", elements[1]);
                        activeTask.Add("destroy", elements[2]);
                        activeTasks.Add(activeTask);
                    }
                }
            }
            return activeTasks;
        }

        public List<Dictionary<string, IWebElement>> GetCompletedTasks()
        {
            List<Dictionary<string, IWebElement>> completedTasks = new List<Dictionary<string, IWebElement>>();
            var alltasks = GetAllTasks();
            if (alltasks.Count != 0)
            {
                foreach (var item in alltasks)
                {
                    if (item.GetAttribute("class") == "completed")
                    {
                        Dictionary<string, IWebElement> completedTask = new Dictionary<string, IWebElement>();
                        var elements = item.FindElements(By.XPath("div[@class='view']/*"));
                        completedTask.Add("toggle", elements[0]);
                        completedTask.Add("label", elements[1]);
                        completedTask.Add("destroy", elements[2]);
                        completedTasks.Add(completedTask);
                    }
                }
            }
            return completedTasks;
        }
        public Dictionary<string, IWebElement> getEditingTask()
        {
            Dictionary<string, IWebElement> elements = new Dictionary<string, IWebElement>();
            if (_todoAppPage.isElementPresent(By.ClassName("todo-list")) && _todoAppPage.isElementPresent(By.XPath("//li[@class='editing']")))
            {
                var className = _todoAppPage.EditingTask.GetAttribute("class");
                var taskElements = _todoAppPage.EditingTask.FindElements(By.XPath("div[@class='view']/*"));
                var toggleType = taskElements[0].GetAttribute("class");
                var labelTagname = taskElements[1].TagName;
                taskElements[1].Click();
                var labelText = taskElements[1].Text;
                //taskElements[1].Clear();
                //taskElements[1].SendKeys("Huaming study Type Script!");
                var buttonClass = taskElements[2].GetAttribute("class");
                elements.Add("toggle", taskElements[0]);
                elements.Add("label", taskElements[1]);
                elements.Add("destory", taskElements[2]);
            }
            return elements;
        }
        public void PerformDoubleClick(IWebElement element)
        {
            Actions act = new Actions(_driver);
            act.DoubleClick(element).Build().Perform();
        }
        public bool ClearCompletedButtonIsPresent()
        {
            return _todoAppPage.isElementPresent(By.ClassName("clear-completed"));
        }
        public bool TodoListIsPresent()
        {
            return _todoAppPage.isElementPresent(By.ClassName("todo-list"));
        }
        public void WaitForSeconds(int delay = 5)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(delay));
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromSeconds(delay) > TimeSpan.Zero);
        }
        public void Dispose()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
