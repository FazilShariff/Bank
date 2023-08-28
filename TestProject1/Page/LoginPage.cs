using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Page
{
    public class LoginPage
    {
        
        public IWebDriver Driver { get; }
        public LoginPage(IWebDriver webdriver)
        {
            Driver = webdriver;
        }

        public IWebElement LinkLogin
        {
            get
            {
                return Driver.FindElement(By.XPath(""));
            }
        }
    }
}
