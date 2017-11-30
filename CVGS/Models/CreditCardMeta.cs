using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(CreditCardMeta))]
    public partial class CREDITCARD
    {
    }

    public class CreditCardMeta
    {
        [DisplayName("ID")]
        public int CardId { get; set; }

        public int MemberId { get; set; }

        [DisplayName("Card Number")]
        [Required, MaxLength(20), DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [DisplayName("Name on Card")]
        [Required, MaxLength(50)]
        public string NameOnCard { get; set; }

        [DisplayName("Expiry")]
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        [DisplayName("Description")]
        [MaxLength(25)]
        public string CardDescription { get; set; }

        [DisplayName("CVV")]
        [Required, MaxLength(4)]
        public string CVV { get; set; }

        [DisplayName("Deleted")]
        [Required]
        public bool Deleted { get; set; }
    }
}