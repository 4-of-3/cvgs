using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(ProvStateMeta))]
    public partial class PROVSTATE
    {
    }

    public class ProvStateMeta
    {
        [DisplayName("ID")]
        public int ProvStateId { get; set; }

        [DisplayName("Province/State Code")]
        public string ProvStateCode { get; set; }

        [DisplayName("Province/State")]
        public string ProvStateName { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }
    }
}