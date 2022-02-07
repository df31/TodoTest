using Xunit;
using System;
using Shouldly;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TODOWebTest.Fixtures;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace TodoWebTest.Steps
{
    [Binding]
    public class TodoAppTestSteps : IClassFixture<TodoAppTestFixture>
    {
        private TodoAppTestFixture _fixture;
        private FeatureContext _context;
        public TodoAppTestSteps(TodoAppTestFixture fixture, FeatureContext context)
        {
            _fixture = fixture;
            _context = context;
        }

        [Given(@"(.*) is typed in")]
        public void GivenIsTypedIn(string taskName)
        {
            _fixture.TodoTestActions.AddTask(taskName);
            _context.Set<string>(taskName, "TaskName");
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [When(@"Enter is pressed")]
        public void WhenEnterIsPressed()
        {
            _fixture.TodoTestActions.PressEnter();
        }

        [Then(@"The task is added")]
        public void ThenTheTaskIsAdded()
        {
            var activeTasks = _fixture.TodoTestActions.GetActiveTasks();
            activeTasks.Count.ShouldBe(1);
            var taskLabel = activeTasks[0]["label"];
            taskLabel.Text.ShouldBe(_context.Get<string>("TaskName"));
        }

        [Then(@"The task is NOT Added")]
        public void ThenTheTaskIsNOTAdded()
        {
            _fixture.TodoTestActions.GetAllTasks().Count.ShouldBe(0);
        }


        [Given(@"One task is added")]
        public void GivenOneTaskIsAdded()
        {
            string taskName = "Study TypeScript";
            _context.Set<string>(taskName, "TaskName");
            _fixture.TodoTestActions.AddTask(taskName);
            _fixture.TodoTestActions.PressEnter();

            _fixture.TodoTestActions.GetAllTasks().Count.ShouldBe(1);
            _fixture.TodoTestActions.GetActiveTasks().Count.ShouldBe(1);
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [When(@"Click on cross button")]
        public void WhenClickOnCrossButton()
        {
            var tasks = _fixture.TodoTestActions.GetActiveTasks();
            tasks.Count.ShouldBe(1);
            var taskLabel = tasks[0]["label"];
            var destoryButton = tasks[0]["destroy"];
            taskLabel.Click();
            destoryButton.Click();
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [Then(@"The task is removed")]
        public void ThenTheTaskIsRemoved()
        {
            _fixture.TodoTestActions.GetAllTasks().Count.ShouldBe(0);
        }

        [When(@"Double click the task")]
        public void WhenDoubleClickTheTask()
        {
            var task = _context.Get<object>("Task1Elements");
            var label = (IWebElement)task.GetType().GetProperty("label").GetValue(task, null);
            var before = label.Text;
            _fixture.TodoTestActions.PerformDoubleClick(label);
            var editingTaskItems = _fixture.TodoTestActions.getEditingTask();
            editingTaskItems["label"].SendKeys(Keys.Backspace);
            _fixture.TodoTestActions.WaitForSeconds(1);
            _fixture.TodoTestActions.AddTask("");
            var after = label.Text;
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [Then(@"The task updated")]
        public void ThenTheTaskUpdated()
        {
            var tasks = _fixture.TodoTestActions.GetActiveTasks();
            tasks.Count.ShouldBe(1);
        }

        [When(@"Mark the task")]
        public void WhenMarkTheTask()
        {
            var activeTasks = _fixture.TodoTestActions.GetActiveTasks();
            var completedTasks = _fixture.TodoTestActions.GetCompletedTasks();
            activeTasks.Count.ShouldBe(1);
            completedTasks.Count.ShouldBe(0);
            var toggle = activeTasks[0]["toggle"];
            toggle.Click();
        }

        [Then(@"The task is marked")]
        public void ThenTheTaskIsMarkedAsCompleted()
        {
            _fixture.TodoTestActions.ClearCompletedButtonIsPresent().ShouldBeTrue();
            var activeTasks = _fixture.TodoTestActions.GetActiveTasks();
            var completedTasks = _fixture.TodoTestActions.GetCompletedTasks();
            activeTasks.Count.ShouldBe(0);
            completedTasks.Count.ShouldBe(1);
        }

        [Given(@"One task is marked as completed")]
        public void GivenOneTaskIsMarkedAsCompleted()
        {
            GivenOneTaskIsAdded();
            WhenMarkTheTask();
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [When(@"unmark the task")]
        public void WhenUnmarkTheTask()
        {
            var completedTasks = _fixture.TodoTestActions.GetCompletedTasks();
            completedTasks.Count.ShouldBe(1);
            var toggle = completedTasks[0]["toggle"];
            toggle.Click();
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [Then(@"The task is unmarked")]
        public void ThenTheTaskIsUnmarked()
        {
            _fixture.TodoTestActions.ClearCompletedButtonIsPresent().ShouldBeFalse();
            _fixture.TodoTestActions.GetActiveTasks().Count.ShouldBe(1);
            _fixture.TodoTestActions.GetCompletedTasks().Count.ShouldBe(0);
        }

        [When(@"Click Clear Completed button")]
        public void WhenClickClearCompletedButton()
        {
            _fixture.TodoTestActions.GetAllTasks().Count.ShouldBe(1);
            _fixture.TodoTestActions.ClickClearCompleted();
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

        [When(@"Click on MarkUnmark")]
        public void WhenClickOnMarkUnmark()
        {
            _fixture.TodoTestActions.ClickMarkUnmark();
            _fixture.TodoTestActions.WaitForSeconds(1);
        }

    }
}
