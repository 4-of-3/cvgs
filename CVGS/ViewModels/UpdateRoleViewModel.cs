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
        private int memberId;
        private int roleId;

        public int MemberId { get => memberId; set => memberId = value; }
        [Display(Name = "Role")]
        public int RoleId { get => roleId; set => roleId = value; }
        public SelectList Roles { get; set; }
    }
}