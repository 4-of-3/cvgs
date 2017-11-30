using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(AddressTypeMeta))]
    public partial class ADDRESSTYPE
    {
    }

    public class AddressTypeMeta
    {
        [DisplayName("ID")]
        public int AddressTypeId { get; set; }

        [DisplayName("Address Type")]
        public string AddressTypeName { get; set; }
    }
}