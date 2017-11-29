using System.ComponentModel.DataAnnotations;

namespace CVGS.ViewModels
{
    public class NewAccountViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        [StringLength(64, MinimumLength =2)]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string LName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(64, MinimumLength = 4)]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        [Required]
        [StringLength(25, MinimumLength =4)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(64, MinimumLength = 6)]
        [CompareAttribute("PwdConfirm", ErrorMessage = "Passwords do not match")]
        public string Pwd { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string PwdConfirm { get; set; }
    }
}