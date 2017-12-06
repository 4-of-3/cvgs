using CVGS.Models;

namespace CVGS.ViewModels
{
    public class GameAssociationsViewModel : GAME
    {
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
    }
}