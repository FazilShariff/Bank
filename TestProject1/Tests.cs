using ECOM.B2B.PageObjects.Core;
using NUnit.Framework;
using Database_Operation.DB;

namespace TestProject1
{
    public class Tests
    {
        #region Positive TestCase

        [Test, Category("A user can have as many accounts as they want")]          
        public void Test1()
        {
            string Account = "Account";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string PostURL = "https://localhost:4565/account";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(Account);

            //Post the request
            string content = serviceRequestApiCalls.RestSharpRequestPOST(PostURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("User1"))
            {
                Assert.True(true);
            }
            Assert.Fail("Account not created");
        }


        [Test, Category("A user can create and delete accounts")]
        public void Test2()
        {

//Creation
            string Account = "Account";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string PostURL = "https://localhost:4565/account/create";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(Account);

            //Post the request
            string content = serviceRequestApiCalls.RestSharpRequestPOST(PostURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("User1"))
            {
                Assert.True(true);
            }
            Assert.Fail("Account not created");

//Deletion
            string DeleteAccount = "DeleteAccount";
            string PostURLdelete = "https://localhost:4565/account/delete";

            //Test Data in json format
            string filePathDelete = serviceRequestApiCalls.Jsonfile(DeleteAccount);

            //Delete the request
            string contentdelete = serviceRequestApiCalls.RestSharpRequestPOST(PostURLdelete, filePath);

            // Read Data
            string Datadelete = serviceRequestApiCalls.ReadData(content);

            if (Datadelete.Contains("User1"))
            {
                Assert.Fail("Account not Deleted"); 
            }
            Assert.True(true);


        }

     
        [Test, Category("A user can deposit and withdraw from accounts")]
        public void Test3()
        {
  
  //Deposit 500$

            SelectStatement DB = new SelectStatement();
            DB.Deposit(500);

            string GET = "Get";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string GetURL = "https://localhost:4565/account/deposit";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(GET);
            //GET the request
            string content = serviceRequestApiCalls.RestSharpRequestGET(GetURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("500"))
            {
                Assert.True(true);
            }
            Assert.Fail("500$ not Credited");


//Withdraw 200$


            DB.Withdraw(200);
            string GetURLwithdraw = "https://localhost:4565/account/withdraw";

            //Test Data in json format
            string filePathWithdraw = serviceRequestApiCalls.Jsonfile(GET);
            //GET the request
            string contentWithdraw = serviceRequestApiCalls.RestSharpRequestGET(GetURLwithdraw, filePathWithdraw);

            // Read Data
            string DataWithdraw = serviceRequestApiCalls.ReadData(contentWithdraw);

            if (DataWithdraw.Contains("300"))
            {
                Assert.True(true);
            }
            Assert.Fail("300$ not Debited");
        
    }

        [Test, Category("An account cannot have less than $100 at any time in an account")]
        
        public void Test4()
        {
            //Total amount 1000$
            //Withdraw All Amount
            SelectStatement DB = new SelectStatement();
            string TotalAmount = DB.Read();

            string TotalAmounts = "TotalAmount";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string GetURL = "https://localhost:4565/account/create";

            //Test Data in json format
            string filePathWithdraw = serviceRequestApiCalls.Jsonfile(TotalAmounts);
            
            //GET the request
            string contentWithdraw = serviceRequestApiCalls.RestSharpRequestGET(GetURL, filePathWithdraw);

            // Read Data
            string DataWithdraw = serviceRequestApiCalls.ReadData(contentWithdraw);

            if (DataWithdraw.Contains("0$"))
            {
                Assert.Fail("An account cannot have less than $100 at any time in an account");
            }
            Assert.True(true);
        }


        [Test, Category("A user cannot withdraw more than 90% of their total balance from an account in a single transaction")]

        public void Test5()
        {
            //Total amount 1000$
            //Withdraw more than 90 Amount
            SelectStatement DB = new SelectStatement();
            string Amount = DB.Read();
            DB.Withdraw(950);

            string TotalAmounts = "TotalAmount";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string GetURL = "https://localhost:4565/account/create";

            //Test Data in json format
            string filePathWithdraw = serviceRequestApiCalls.Jsonfile(TotalAmounts);

            //GET the request
            string contentWithdraw = serviceRequestApiCalls.RestSharpRequestGET(GetURL, filePathWithdraw);

            // Read Data
            string DataWithdraw = serviceRequestApiCalls.ReadData(contentWithdraw);

            if (DataWithdraw.Contains("50$"))
            {
                Assert.Fail("A user cannot withdraw more than 90% of their total balance from an account in a single transaction");
            }
            Assert.True(true);
        }

        
        [Test, Category("A user cannot deposit more than $10000 in a single transaction")]
        public void Test6()
        {

            //Deposit 10001$

            SelectStatement DB = new SelectStatement();
            DB.Deposit(10001);

            string GET = "Get";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string GetURL = "https://localhost:4565/account/create";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(GET);
            //GET the request
            string content = serviceRequestApiCalls.RestSharpRequestGET(GetURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("10001"))
            {
                Assert.Fail("cannot deposit more than $10000");
                
            }
            Assert.True(true);

        }

        #endregion




        #region Negetive TestCase

        [Test, Category("A user can'nt create more than one account")]
        public void Test7()
        {
            string MultipleAccounts = "MultipleAccounts";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string PostURL = "https://localhost:4565/account";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(MultipleAccounts);

            //Post the request
            string content = serviceRequestApiCalls.RestSharpRequestPOST(PostURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("User,User1,User3"))
            {
                Assert.True(true);
            }
            Assert.Fail("Unable to create multiple Accounts");
        }


        [Test, Category("A user can'nt delete accounts")]
        public void Test8()
        {

            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            //Deletion
            string DeleteAccount = "DeleteAccount";
            string PostURLdelete = "https://localhost:4565/account/delete";

            //Test Data in json format
            string filePathDelete = serviceRequestApiCalls.Jsonfile(DeleteAccount);

            //Delete the request
            string contentdelete = serviceRequestApiCalls.RestSharpRequestPOST(PostURLdelete, filePathDelete);

            // Read Data
            string Datadelete = serviceRequestApiCalls.ReadData(contentdelete);

            if (Datadelete.Contains("User1"))
            {
                Assert.Fail("Account not Deleted");
            }
            Assert.True(true);


        }


        [Test, Category("A user not able to deposit  from accounts.")]
        public void Test9()
        {
            //Total Balance 1000$
            //Deposit 500$

            SelectStatement DB = new SelectStatement();
            DB.Deposit(500);

            string GET = "Get";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();
            string GetURL = "https://localhost:4565/account/deposit";

            //Test Data in json format
            string filePath = serviceRequestApiCalls.Jsonfile(GET);
            //GET the request
            string content = serviceRequestApiCalls.RestSharpRequestGET(GetURL, filePath);

            // Read Data
            string Data = serviceRequestApiCalls.ReadData(content);

            if (Data.Contains("1500$"))
            {
                Assert.True(true);
            }
            Assert.Fail("500$ not Credited");



        }

        [Test, Category("A user not able to withdraw from accounts.")]
        public void Test10()
        {

            //Total Balance 1500$
            //Withdraw 200$

            SelectStatement DB = new SelectStatement();
            DB.Withdraw(200);
            string GetURLwithdraw = "https://localhost:4565/account/withdraw";
            ServiceRequestAPICalls serviceRequestApiCalls = new ServiceRequestAPICalls();

            //Test Data in json format
            string filePathWithdraw = serviceRequestApiCalls.Jsonfile(GetURLwithdraw);
            //GET the request
            string contentWithdraw = serviceRequestApiCalls.RestSharpRequestGET(GetURLwithdraw, filePathWithdraw);

            // Read Data
            string DataWithdraw = serviceRequestApiCalls.ReadData(contentWithdraw);

            if (DataWithdraw.Contains("1300"))
            {
                Assert.True(true);
            }
            Assert.Fail("300$ not Debited");

        }

        #endregion

    }

}