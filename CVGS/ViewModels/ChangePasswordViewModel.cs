using CVGS.Models;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int MemberId { get; set; }

        [Display(Name = "Old Password")]
        [Required]
        public string OldPwd { get; set; }

        [Display(Name = "New Password")]
        [Required]
        [CompareAttribute("NewPwdCheck", ErrorMessage = "Passwords do not match")]
        public string NewPwd { get; set; }

        [Display(Name = "Confirm  Password")]
        [Required]
        public string NewPwdCheck { get; set; }


    }
}