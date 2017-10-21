using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVGS.Models
{
    [MetadataType(typeof(GameMeta))]
    public partial class GAME
    {
    }
 
    public class GameMeta
    {
        [DisplayName("ID")]
        public int GameId { get; set; }

        [DisplayName("Title")]
        [Required]
        public string Title { get; set; }

        [DisplayName("ISBN")]
        [Required, MaxLength(10)]
        public string ISBN { get; set; }

        [DisplayName("Developer")]
        [Required]
        public string Developer { get; set; }

        [DisplayName("Description")]
        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [DisplayName("Category")]
        [Required]
        public string Category { get; set; }
        [DisplayName("Image URL")]
        public string ImageUrl { get; set; }

        [DisplayName("Published")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [DisplayName("Cost")]
        [Required, DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [DisplayName("Digital")]
        [Required]
        public bool Digital { get; set; }
    }
}