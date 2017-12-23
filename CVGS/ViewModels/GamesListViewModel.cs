using CVGS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class GamesListViewModel
    {
        public int MemberId { get; set; }
        public List<GAME> Games { get; set; }
    }
}