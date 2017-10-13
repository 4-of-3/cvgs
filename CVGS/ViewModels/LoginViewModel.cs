﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class LoginViewModel
    {
        private string userName;
        private string pwd;

        [Display(Name ="User Name")]
        [Required]
        public string UserName { get => userName; set => userName = value; }

        [Display(Name ="Password")]
        [Required]
        public string Pwd { get => pwd; set => pwd = value; }
    }
}