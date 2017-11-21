using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(EventMeta))]
    public partial class EVENT
    {
    }

    public class EventMeta
    {
        [DisplayName("ID")]
        public int EventId { get; set; }

        [DisplayName("Title")]
        [Required]
        public string EventTitle { get; set; }

        [DisplayName("Description")]
        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [DisplayName("Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        public System.DateTime EventDate { get; set; }

        [DisplayName("Active")]
        public bool ActiveStatus { get; set; }

        [DisplayName("Date Created")]
        public Nullable<System.DateTime> DateCreated { get; set; }

        [DisplayName("Location")]
        [Required, MaxLength(64)]
        public string Location { get; set; }
    }
}