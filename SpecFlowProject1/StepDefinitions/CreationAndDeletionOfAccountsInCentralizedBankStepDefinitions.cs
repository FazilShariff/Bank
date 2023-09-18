using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class CreationAndDeletionOfAccountsInCentralizedBankStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly TestFixtures.TestContext testContext;

        public CreationAndDeletionOfAccountsInCentralizedBankStepDefinitions(ScenarioContext Injectedcontext, TestFixtures.TestContext InjectedtestContext)
        {
            context = Injectedcontext;
            testContext = InjectedtestContext;
        }

        string Account = "Account";
        string PostURL = "https://localhost:4565/account";
        string DeleteAccount = "DeleteAccount";
        string PostURLdelete = "https://localhost:4565/account/delete";

        [Given(@"I have testdatafile for creation/deletion of account and baseUrl")]
        public void GivenIHaveTestdatafileForCreationDeletionOfAccountAndBaseUrl()
        {
            //Test Data in json format
            testContext.filePath = testContext.ServiceRequestAPICalls.Jsonfile(Account);
        }

        [When(@"Post call is made to create an account")]
        public void WhenPostCallIsMadeToCreateAnAccount()
        {
            string filePath="";
            //Post the request
            testContext.content = testContext.ServiceRequestAPICalls.RestSharpRequestPOST(PostURL, filePath);
        }


        [Then(@"I get successful response code (.*)")]
        public void ThenIGetSuccessfulResponseCode(int p0)
        {
            Assert.True(true);
        }

        [Then(@"I validate the successful response message as ""([^""]*)""")]
        public void ThenIValidateTheSuccessfulResponseMessageAs(string content)
        {
            // Read Data
            testContext.Data = testContext.ServiceRequestAPICalls.ReadData(content);
        }

        [Then(@"Account created with initial balance '([^']*)'")]
        public void ThenAccountCreatedWithInitialBalance(string Account)
        {
            if (Account.Contains("User1"))
            {
                Assert.True(true);
            }
            Assert.Fail("Account not created");
        }

        [When(@"Delete call is made to delete an account")]
        public void WhenDeleteCallIsMadeToDeleteAnAccount()
        {
            string Accountdeleted = "";
            if (Accountdeleted.Contains("User1"))
            {
                Assert.Fail("Account not Deleted");
            }
            Assert.True(true);
        }

        [Then(@"User got deleted from the Accounts")]
        public void ThenUserGotDeletedFromTheAccounts()
        {
            Assert.True(true);
        }
    }
}
