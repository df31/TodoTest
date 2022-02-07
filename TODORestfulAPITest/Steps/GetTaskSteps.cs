using Xunit;
using System;
using Shouldly;
using RestSharp;
using TechTalk.SpecFlow;
using TodoAPITest.Fixtures;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TodoAPITest.Modules;
using Newtonsoft.Json;
using System.Net;

namespace TodoAPITest.Steps
{
    [Binding]
    public class GetTaskSteps : IClassFixture<TodoFixture>
    {
        private TodoFixture fixture;

        public GetTaskSteps(TodoFixture fixture)
        {
            this.fixture = fixture;
        }


        [Given(@"user wants to get all the task")]
        public void GivenUserWantToGetAllTheTask()
        {
            fixture.request = new RestRequest("", Method.Get);
        }

        [Then(@"the result should be fetched")]
        public void ThenTheResultShouldBeFetched()
        {
            var content = JsonConvert.DeserializeObject<List<Item>>(fixture.response.Content);
            content.Count.ShouldBe(200);
        }

        [Given(@"(.*) tasks is wanted")]
        public void GivenTasksIsWanted(string userId)
        {
            fixture.request = new RestRequest("", Method.Get).AddQueryParameter("userId", userId);
        }

        [Then(@"The user's tasks should be fetched")]
        public void ThenTheUserSTaskShouldBeFetched()
        {
            var content = JsonConvert.DeserializeObject<List<Item>>(fixture.response.Content);
            content.Count.ShouldNotBe(0);
        }


    }
}
