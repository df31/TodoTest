using Xunit;
using System;
using Shouldly;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using TodoAPITest.Modules;
using TodoAPITest.Fixtures;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoAPITest.Steps
{
    [Binding]
    public class AddTaskSteps : IClassFixture<TodoFixture>
    {
        private TodoFixture fixture;
        private FeatureContext context;
        public AddTaskSteps(TodoFixture fixture, FeatureContext context)
        {
            this.fixture = fixture;
            this.context = context;
        }

        [Given(@"user wants to add new task")]
        public void GivenUserWantsToAddNewTask()
        {
            Item newTask = new Item
            {
                userId = 6,
                id = 201,
                title = "blablabla",
                completed = false
            };
            context.Set<Item>(newTask, "NewTask");
            fixture.request = new RestRequest("", Method.Post);
            fixture.request.RequestFormat = DataFormat.Json;
            fixture.request.AddBody(newTask);
        }

        [Then(@"the task is added")]
        public void ThenTheTaskIsAdded()
        {
            fixture.response.StatusCode.ShouldBe(HttpStatusCode.Created);
            var body = JsonConvert.DeserializeObject<Item>(fixture.response.Content);
            body.ShouldBeEquivalentTo(context.Get<Item>("NewTask"));
        }

    }
}
