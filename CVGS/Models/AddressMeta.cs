using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(AddressMeta))]
    public partial class ADDRESS
    {
    }

    public class AddressMeta
    {
        [DisplayName("ID")]
        public int AddressId { get; set; }

        public int MemberId { get; set; }

        [DisplayName("Street Address")]
        [Required, MaxLength(1024)]
        public string StreetAddress { get; set; }

        [DisplayName("Secondary Address")]
        public string StreetAddress2 { get; set; }

        [DisplayName("City"), MaxLength(100)]
        [Required]
        public string City { get; set; }

        [DisplayName("Postal Code")]
        [Required, MaxLength(15)]
        public string PostCode { get; set; }

        [DisplayName("Province/State")]
        [Required]
        public int ProvStateId { get; set; }

        [DisplayName("Address Type")]
        [Required]
        public int AddressTypeId { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }
    }
}