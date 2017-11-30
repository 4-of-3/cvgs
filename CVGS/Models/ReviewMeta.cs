using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Review")]
        [Required, StringLength(1024)]
        public string ReviewText { get; set; }

        [DisplayName("Rating")]
        [Required, Range(1,5)]
        public int Rating { get; set; }

        [DisplayName("Date Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [DisplayName("Date Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateModified { get; set; }
    }
}