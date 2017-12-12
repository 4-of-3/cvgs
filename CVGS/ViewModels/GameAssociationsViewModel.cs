using CVGS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(GameMeta))]
    public class GameAssociationsViewModel : GAME
    {
        public IEnumerable<REVIEW> ApprovedReviews;

        /// <summary>
        /// Game's average rating
        /// </summary>
        public double AvgRating;

        /// <summary>
        /// Whether the Game is on the Member's Wishlist
        /// </summary>
        public bool OnWishlist;

        /// <summary>
        /// Whether the Game is in the Members' Cart
        /// </summary>
        public bool InCart;

        /// <summary>
        /// Whether the Member has Purchased the Game
        /// </summary>
        public bool Purchased;


        /// <summary>
        /// Extend the base Game class with basic association information about a game (average rating, in wishlist, purchased, etc)
        /// </summary>
        /// <param name="game">Game model object to convert</param>
        /// <param name="memberId">Member id that defines several associations</param>
        /// <returns>Extended viewmodel for Game model with associations</returns>
        public static GameAssociationsViewModel CreateGameAssociationFromModel(GAME game, int memberId)
        {
            // Add associated game data
            double avgRating = -1;
            bool isInCart = false;
            bool isPurchased = false;
            bool isOnWishlist = false;


            IEnumerable<REVIEW> approvedReviews = game.REVIEWs.Where(r => r.Approved);
            if (approvedReviews.Any())
            {
                avgRating = Math.Round(approvedReviews.Average(g => g.Rating), 2);
            }
            isInCart = game.CARTITEMs.Select(c => c.MemberId).ToList().Contains(memberId);
            isOnWishlist = game.WISHLISTITEMs.Select(w => w.MemberId).ToList().Contains(memberId);

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
                // Associated information (added)
                ApprovedReviews = approvedReviews,
                AvgRating = avgRating,
                InCart = isInCart,
                Purchased = isPurchased,
                OnWishlist = isOnWishlist,
                // Associated lists
                PLATFORMs = game.PLATFORMs,
                REVIEWs = game.REVIEWs,
                CARTITEMs = game.CARTITEMs,
                ORDERITEMs = game.ORDERITEMs,
                WISHLISTITEMs = game.WISHLISTITEMs
            };

            return gameWithAssociations;
        }


        /// <summary>
        /// Convert a list of Games models into a list of the extended Games viewmodel with assocations
        /// </summary>
        /// <param name="games">List of Game models</param>
        /// <param name="memberId">Member id that defines several assocations</param>
        /// <returns>List of extended viewmodels for Game model with associations</returns>
        public static List<GameAssociationsViewModel> CreateGameAssociationsListFromModels(IEnumerable<GAME> games, int memberId)
        {
            List<GameAssociationsViewModel> gameAssociations = new List<GameAssociationsViewModel>();

            foreach (GAME game in games)
            {
                gameAssociations.Add(CreateGameAssociationFromModel(game, memberId));
            }

            return gameAssociations;
        }
    }
}