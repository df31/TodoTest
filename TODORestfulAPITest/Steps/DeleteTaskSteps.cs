using Xunit;
using System;
using Shouldly;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using TodoAPITest.Fixtures;
using System.Threading.Tasks;

namespace TodoAPITest.Steps
{
   [Binding]
    public class DeleteTaskSteps : IClassFixture<TodoFixture>
    {
        private TodoFixture fixture;
        public DeleteTaskSteps(TodoFixture fixture, FeatureContext context)
        {
            this.fixture = fixture;
        }

        [Given(@"user wants to delete task (.*)")]
        public void GivenUserWantsToDeleteATask(string id)
        {
            fixture.request = new RestRequest("/{id}", Method.Delete);
            fixture.request.AddUrlSegment("id", id);
        }

        [Then(@"the task is removed")]
        public void ThenTheTaskIsDeleted()
        {
            fixture.response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
