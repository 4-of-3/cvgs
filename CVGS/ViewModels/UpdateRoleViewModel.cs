using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVGS.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CVGS.ViewModels
{
    public class UpdateRoleViewModel
    {
        public int MemberId { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public SelectList Roles { get; set; }
    }
}