using Xunit;
using System;
using TechTalk.SpecFlow;
using TodoAPITest.Modules;
using TodoAPITest.Fixtures;
using RestSharp;
using System.Threading.Tasks;
using Shouldly;
using System.Net;
using Newtonsoft.Json;

namespace TodoAPITest.Steps
{
    [Binding]
    public class EditTaskSteps : IClassFixture<TodoFixture>
    {
        private TodoFixture fixture;
        private FeatureContext context;
        public EditTaskSteps(TodoFixture fixture, FeatureContext context)
        {
            this.fixture = fixture;
            this.context = context;
        }

        [Given(@"user wants to update task")]
        public void GivenUserWantsToUpdateTask()
        {
            Item updatedTask = new Item
            {
                userId = 1,
                id = 1,
                title = "recent updated task",
                completed = false
            };
            context.Set<Item>(updatedTask, "UpdatedTask");
            fixture.request = new RestRequest("/{id}", Method.Put);
            fixture.request.AddUrlSegment("id", updatedTask.id);
            fixture.request.RequestFormat = DataFormat.Json;
            fixture.request.AddBody(updatedTask);
        }

        [Then(@"the task is updated")]
        public void ThenTheTaskIsUpdated()
        {
            fixture.response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<Item>(fixture.response.Content);
            body.ShouldBeEquivalentTo(context.Get<Item>("UpdatedTask"));
        }

    }
}
