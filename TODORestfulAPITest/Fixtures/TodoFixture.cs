using System;
using RestSharp;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using Shouldly;
using System.Net;

namespace TodoAPITest.Fixtures
{
    [Binding]
    public class TodoFixture
    {
        public RestClient client;
        public RestRequest request;
        public RestResponse response;

        public TodoFixture()
        {
            client = new RestClient(Startup.Config["TodoApi:HostUrl"]);
        }

        [When(@"the request is sent")]
        public async Task WhenTheRequestIsSent()
        {
            response = await client.ExecuteAsync(request);
        }

    }
}
