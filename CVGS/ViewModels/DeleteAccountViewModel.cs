using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class DeleteAccountViewModel
    {
        private int memberId;
        private string userName;
        private bool fullDelete;

        public int MemberId { get => memberId; set => memberId = value; }
        public string UserName { get => userName; set => userName = value; }
        public bool FullDelete { get => fullDelete; set => fullDelete = value; }
    }
}