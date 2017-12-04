using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    [MetadataType(typeof(CartMeta))]
    public partial class CARTITEM
    {
    }
    public class CartMeta
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [DisplayName("Date Added")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }
    }
}