using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    [MetadataType(typeof(ReviewMeta))]
    public partial class REVIEW
    {
    }
    public class ReviewMeta
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int GameId { get; set; }
        [Display(Name = "Review")]
        [StringLength(1024)]
        public string ReviewText { get; set; }
        [Display(Name = "Rating")]
        [Required, Range(1,5)]
        public int Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public System.DateTime DateCreated { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}