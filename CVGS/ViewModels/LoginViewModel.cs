using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class LoginViewModel
    {
        private string userName;
        private string pwd;

        public string Pwd { get => pwd; set => pwd = value; }
        public string UserName { get => userName; set => userName = value; }
    }
}