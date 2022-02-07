using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Configuration;

namespace TodoAPITest
{
    [Binding]
    public class Startup
    {
        public static IConfiguration Config { get; private set; }

        [BeforeTestRun]
        public static void InitConfiguration()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
