using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class EditAccountViewModel
    {
        public int MemberId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LName { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required, DataType(DataType.EmailAddress), StringLength(60)]
        public string Email { get; set; }

        [Display(Name = "Favourite Platform")]
        [StringLength(60)]
        public string FavPlatform { get; set; }

        [Display(Name = "Favourite Category")]
        [StringLength(60)]
        public string FavCategory { get; set; }

        [Display(Name = "Favourite Game")]
        [StringLength(60)]
        public string FavGame { get; set; }

        [Display(Name ="Favourite Quote")]
        [StringLength(140)]
        public string FavQuote { get; set; }
    }
}