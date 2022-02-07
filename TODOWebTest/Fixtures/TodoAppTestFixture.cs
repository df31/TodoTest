using System;
using TodoWebTest;
using TechTalk.SpecFlow;
using TODOWebTest.PageActions;

namespace TODOWebTest.Fixtures
{
    [Binding]
    public class TodoAppTestFixture : IDisposable
    {
        public TodoAppActions TodoTestActions { get; set; }
        public TodoAppTestFixture()
        {
            TodoTestActions = new TodoAppActions(WebDriverFactory.GetChromeDriver(), Startup.Config);
        }
        public void Dispose()
        {
            TodoTestActions.Dispose();
        }
    }
}
