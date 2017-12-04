using CVGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class CartViewModel
    {
        public int MemberId { get; set; }
        public List<CARTITEM> CartItems { get; set; }
    }
}