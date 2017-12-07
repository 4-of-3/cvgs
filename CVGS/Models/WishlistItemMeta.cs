using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(WishlistItemMeta))]
    public partial class WISHLIST
    {
    }

    public class WishlistItemMeta
    {
        [DisplayName("Member ID")]
        public int MemberId { get; set; }

        [DisplayName("Game ID")]
        public int GameId { get; set; }
        
        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }
    }
}