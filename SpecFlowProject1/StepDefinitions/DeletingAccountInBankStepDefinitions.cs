using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class DeletingAccountInBankStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly TestFixtures.TestContext testContext;

        public DeletingAccountInBankStepDefinitions(ScenarioContext Injectedcontext, TestFixtures.TestContext InjectedtestContext)
        {
            context = Injectedcontext;
            testContext = InjectedtestContext;
        }

        string DeleteAccount = "DeleteAccount";
        string PostURLdelete = "https://localhost:4565/account/delete";

        [Given(@"Post the delete request ""([^""]*)""")]
        public void GivenPostTheDeleteRequest(string filePathdelete)
        {
            //Post the request
            testContext.content = testContext.ServiceRequestAPICalls.RestSharpRequestPOST(PostURLdelete, filePathdelete);
        }

        [When(@"Read deleted data  ""([^""]*)""")]
        public void WhenReadDeletedData(string contentdelete)
        {
            // Read Data
            testContext.Data = testContext.ServiceRequestAPICalls.ReadData(contentdelete);
        }

        [Then(@"Validate deleted data ""([^""]*)""")]
        public void ThenValidateDeletedData(string datadelete)
        {
            if (datadelete.Contains("User1"))
            {
                Assert.Fail("Account not Deleted");
            }
            Assert.True(true);
        }
    }
}
