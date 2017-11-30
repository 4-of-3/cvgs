using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(CountryMeta))]
    public partial class COUNTRY
    {
    }

    public class CountryMeta
    {
        [DisplayName("ID")]
        public int CountryId { get; set; }

        [DisplayName("Country Code")]
        public string CountryCode { get; set; }

        [DisplayName("Country")]
        public string CountryName { get; set; }
    }
}