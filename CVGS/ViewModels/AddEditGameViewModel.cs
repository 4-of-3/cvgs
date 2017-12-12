using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class AddEditGameViewModel
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
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [DisplayName("Cost")]
        [Required, DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [DisplayName("Digital")]
        [Required]
        public bool Digital { get; set; }
        [DisplayName("Platforms")]
        [Required(ErrorMessage = "Games must be playable on at least one platform")]
        public List<int> Platforms { get; set; }
    }
}