using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(MemberMeta))]
    public partial class MEMBER
    {
    }

    public class MemberMeta
    {
        [DisplayName("ID")]
        public int MemberId { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LName { get; set; }

        [DisplayName("Username")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Favourite Platform")]
        public string FavPlatform { get; set; }

        [DisplayName("Favourite Category")]
        public string FavCategory { get; set; }

        [DisplayName("Favourite Game")]
        public string FavGame { get; set; }

        [DisplayName("Favourite Quote")]
        public string FavQuote { get; set; }

        [DisplayName("Date Joined")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateJoined { get; set; }

        [DisplayName("Active")]
        [Required]
        public bool ActiveStatus { get; set; }

        [DisplayName("Password")]
        [Required]
        public byte[] Pwd { get; set; }
    }
}