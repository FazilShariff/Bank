using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class CreatingAccountInBankStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly TestFixtures.TestContext testContext;

        public CreatingAccountInBankStepDefinitions(ScenarioContext Injectedcontext, TestFixtures.TestContext InjectedtestContext)
        {
            context = Injectedcontext;
            testContext = InjectedtestContext;
        }

        string Account = "Account";
        string PostURL = "https://localhost:4565/account";


        [Given(@"Pick TestData, PostURL And retrive test data in json format")]
        public void GivenPickTestDataPostURLAndRetriveTestDataInJsonFormat()
        {
            //Test Data in json format
            testContext.filePath = testContext.ServiceRequestAPICalls.Jsonfile(Account);
        }

        [Given(@"Post the request ""([^""]*)""")]
        public void GivenPostTheRequest(string filePath)
        {
            //Post the request
            testContext.content = testContext.ServiceRequestAPICalls.RestSharpRequestPOST(PostURL, filePath);
        }

        [When(@"Read data ""([^""]*)""")]
        public void WhenReadData(string content)
        {
            // Read Data
            testContext.Data = testContext.ServiceRequestAPICalls.ReadData(content);
        }

        [Then(@"Validate data ""([^""]*)""")]
        public void ThenValidateData(string data)
        {
            if (data.Contains("User1"))
            {
                Assert.True(true);
            }
            Assert.Fail("Account not created");
        }

    }
}
