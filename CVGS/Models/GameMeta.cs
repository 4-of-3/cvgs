using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CVGS.ViewModels;

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
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [DisplayName("Cost")]
        [Required, DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [DisplayName("Digital")]
        [Required]
        public bool Digital { get; set; }


        /// <summary>
        /// Extend the base Game class with basic association information about a game (average rating, in wishlist, purchased, etc)
        /// </summary>
        /// <param name="game">Game model object to convert</param>
        /// <param name="memberId">Member id that defines several associations</param>
        /// <returns></returns>
        public static GameAssociationsViewModel CreateGameAssociationFromModel(GAME game, int memberId)
        {
            // Add associated game data
            double avgRating = -1;
            bool isInCart = false;
            bool isPurchased = false;
            bool isOnWishlist = false;

            if (game.REVIEWs.Any())
            {
                avgRating = Math.Round(game.REVIEWs.Average(g => g.Rating), 2);
            }
            isInCart = game.CARTITEMs.Select(c => c.MemberId).ToList().Contains(memberId);

            // Create custom view model to display game associations (avg reviews, purchased, in cart, etc)
            GameAssociationsViewModel gameWithAssociations = new GameAssociationsViewModel()
            {
                GameId = game.GameId,
                Title = game.Title,
                ISBN = game.ISBN,
                Developer = game.Developer,
                Category = game.Category,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                PublicationDate = game.PublicationDate,
                Cost = game.Cost,
                Digital = game.Digital,
                Deleted = game.Deleted,
                AvgRating = avgRating,
                InCart = isInCart,
                Purchased = isPurchased,
                OnWishlist = isOnWishlist
            };

            return gameWithAssociations;
        }
    }
}