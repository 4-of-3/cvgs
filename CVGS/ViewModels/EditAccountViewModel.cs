using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class EditAccountViewModel
    {
        private int memberId;
        private string fName;
        private string lName;
        private string userName;
        private string email;
        private string favPlatform;
        private string favCategory;
        private string favGame;
        private string favQuote;

        public int MemberId { get => memberId; set => memberId = value; }
        [Display(Name = "First Name")]
        [Required]
        public string FName { get => fName; set => fName = value; }
        [Display(Name = "Last Name")]
        [Required]
        public string LName { get => lName; set => lName = value; }
        [Display(Name = "User Name")]
        public string UserName { get => userName; set => userName = value; }
        [Display(Name = "Email")]
        [Required, DataType(DataType.EmailAddress), StringLength(60)]
        public string Email { get => email; set => email = value; }
        [Display(Name = "Favourite Platform")]
        [StringLength(60)]
        public string FavPlatform { get => favPlatform; set => favPlatform = value; }
        [Display(Name = "Favourite Category")]
        [StringLength(60)]
        public string FavCategory { get => favCategory; set => favCategory = value; }
        [Display(Name = "Favourite Game")]
        [StringLength(60)]
        public string FavGame { get => favGame; set => favGame = value; }
        [Display(Name ="Favourite Quote")]
        [StringLength(140)]
        public string FavQuote { get => favQuote; set => favQuote = value; }
    }
}