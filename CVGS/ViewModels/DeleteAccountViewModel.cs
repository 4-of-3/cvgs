using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class DeleteAccountViewModel
    {
        public int MemberId { get; set; }
        public string UserName { get; set; }
        public bool FullDelete { get; set; }
    }
}