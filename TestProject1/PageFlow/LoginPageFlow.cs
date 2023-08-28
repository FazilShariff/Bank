using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Page;

namespace TestProject1.WorkFlow
{
    public class LoginPageFlow
    {
        private readonly LoginPage _pageInstance;

        public LoginPageFlow()
        {
        }

        public LoginPageFlow(LoginPage page)
        {
            _pageInstance = page;
        }

        public LoginPage PageInstance
        {
            get
            {
                return _pageInstance;
            }
        }

        public void Add()
        {
            PageInstance.LinkLogin.Click(); 

        }

    }
}
