using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(MemberMeta))]
    public partial class MEMBER
    {
        [DisplayName("Name")]
        public string FullName
        {
            get { return FName + " " + LName; }
        }
    }

    public class MemberMeta
    {
        [DisplayName("ID")]
        public int MemberId { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(64, MinimumLength =2 )]
        public string FName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string LName { get; set; }

        [DisplayName("Username")]
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required, DataType(DataType.EmailAddress)]
        [StringLength(64, MinimumLength = 6)]
        public string Email { get; set; }

        [DisplayName("Favourite Platform")]
        [StringLength(25)]
        public string FavPlatform { get; set; }

        [DisplayName("Favourite Category")]
        [StringLength(25)]
        public string FavCategory { get; set; }

        [DisplayName("Favourite Game")]
        [StringLength(64)]
        public string FavGame { get; set; }

        [DisplayName("Favourite Quote")]
        [StringLength(140)]
        public string FavQuote { get; set; }

        [DisplayName("Date Joined")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateJoined { get; set; }

        [DisplayName("Active")]
        [Required]
        public bool ActiveStatus { get; set; }

        [DisplayName("Password")]
        [Required]
        public byte[] Pwd { get; set; }
    }
}