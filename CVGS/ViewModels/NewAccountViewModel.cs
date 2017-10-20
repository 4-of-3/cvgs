using System.ComponentModel.DataAnnotations;

namespace CVGS.ViewModels
{
    public class NewAccountViewModel
    {
        private string fName;
        private string lName;
        private string email;
        private string userName;
        private string pwd;
        private string pwdConfirm;
        private string favPlatform;
        private string favCategory;
        private string favGame;
        private string favQuote;

        [Display(Name = "First Name")]
        [Required]
        [StringLength(64, MinimumLength =2)]
        public string FName { get => fName; set => fName = value; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string LName { get => lName; set => lName = value; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(64, MinimumLength = 4)]
        public string Email { get => email; set => email = value; }

        [Display(Name = "User Name")]
        [Required]
        [StringLength(25, MinimumLength =4)]
        public string UserName { get => userName; set => userName = value; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Pwd { get => pwd; set => pwd = value; }

        [Display(Name = "Confirm Password")]
        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string PwdConfirm { get => pwdConfirm; set => pwdConfirm = value; }

        [Display(Name = "Favourite Gaming Platform")]
        [StringLength(25)]
        public string FavPlatform { get => favPlatform; set => favPlatform = value; }

        [Display(Name = "Favourite Game Category")]
        [StringLength(25)]
        public string FavCategory { get => favCategory; set => favCategory = value; }

        [Display(Name = "Favourite Game")]
        [StringLength(64)]
        public string FavGame { get => favGame; set => favGame = value; }

        [Display(Name = "Favourite Gaming Quote")]
        [StringLength(140)]
        public string FavQuote { get => favQuote; set => favQuote = value; }
    }
}