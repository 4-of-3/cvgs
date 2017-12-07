using CVGS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(WishlistItemMeta))]
    public class WishlistItemViewModel : WISHLISTITEM
    {
        private string description;

        /// <summary>
        /// Wishlist game category
        /// </summary>
        [DisplayName("Category")]
        public string Category { get; set; }

        /// <summary>
        /// Wishlist game title
        /// </summary>
        [DisplayName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Wishlist game description
        /// </summary>
        [DisplayName("Description")]
        public string Description {
            get {
                return description.Length > 100
                    ? description.Substring(0, 50) + "..."
                    : description;
            }
            set { description = value; }
        }

        /// <summary>
        /// Wishlist game cost
        /// </summary>
        [DisplayName("Cost")]
        public decimal Cost { get; set; }
        
        /// <summary>
        /// Whether wishlist game is digital
        /// </summary>
        [DisplayName("Digital")]
        public bool Digital { get; set; }

        /// <summary>
        /// Wishlist game average rating
        /// </summary>
        [DisplayName("Average Rating")]
        public double AvgRating { get; set; }


        /// <summary>
        /// Extend the base WishlistItem class with basic association information about an item (game title, category, average rating, etc)
        /// </summary>
        /// <param name="wishlistItem">Wishlist item model object to convert</param>
        /// <returns>Extended viewmodel for WishlistItem model with associations</returns>
        public static WishlistItemViewModel CreateWishlistItemAssociationFromModel(WISHLISTITEM wishlistItem)
        {
            double avgRating = 0;

            if (wishlistItem.GAME.REVIEWs.Count() > 0)
            {
                avgRating = Math.Round(wishlistItem.GAME.REVIEWs.Average(w => w.Rating), 2);
            }

            WishlistItemViewModel wishlistItemViewModel = new WishlistItemViewModel()
            {
                GameId = wishlistItem.GameId,
                MemberId = wishlistItem.MemberId,
                Title = wishlistItem.GAME.Title,
                Description = wishlistItem.GAME.Description,
                Category = wishlistItem.GAME.Category,
                Cost = wishlistItem.GAME.Cost,
                Digital = wishlistItem.GAME.Digital,
                DateAdded = wishlistItem.DateAdded,
                // Associated information
                AvgRating = avgRating,
                // Associated lists
                GAME = wishlistItem.GAME,
                MEMBER = wishlistItem.MEMBER
            };

            return wishlistItemViewModel;
        }

        /// <summary>
        /// Convert a list of WishlistItem models into a list of the extended WishlistItem viewmodel with assocations
        /// </summary>
        /// <param name="wishlistItems">List of WishlistItem models</param>
        /// <returns>List of extended viewmodels for WishlistItem model with associations</returns>
        public static List<WishlistItemViewModel> CreateWishlistItemAssociationsListFromModels(IEnumerable<WISHLISTITEM> wishlistItems)
        {
            List<WishlistItemViewModel> wishlistItemAssociations = new List<WishlistItemViewModel>();

            foreach (WISHLISTITEM wishlistItem in wishlistItems)
            {
                wishlistItemAssociations.Add(CreateWishlistItemAssociationFromModel(wishlistItem));
            }

            return wishlistItemAssociations;
        }
    }
}