using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class NewAccountViewModel
    {
        private string fName;
        private string lName;
        private string email;
        private string userName;
        private string pwd;
        private string favPlatform;
        private string favCategory;
        private string favGame;
        private string favQuote;

        [Display(Name = "First Name")]
        [Required]
        public string FName { get => fName; set => fName = value; }
        [Display(Name = "Last Name")]
        [Required]
        public string LName { get => lName; set => lName = value; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get => email; set => email = value; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get => userName; set => userName = value; }
        [Display(Name = "Password")]
        [Required]
        public string Pwd { get => pwd; set => pwd = value; }
        [Display(Name = "Favourite Gaming Platform")]
        public string FavPlatform { get => favPlatform; set => favPlatform = value; }
        [Display(Name = "Favourite Game Category")]
        public string FavCategory { get => favCategory; set => favCategory = value; }
        [Display(Name = "Favourite Game")]
        public string FavGame { get => favGame; set => favGame = value; }
        [Display(Name = "Favourite Gaming Quote")]
        public string FavQuote { get => favQuote; set => favQuote = value; }

    }
}