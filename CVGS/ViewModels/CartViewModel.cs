using CVGS.Models;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class CartViewModel
    {
        public int MemberId { get; set; }
        [Required]
        [ItemQuantityMin(MinQuantity = 0,ErrorMessage = "Quantity cannot be less than 0")]
        public List<CARTITEM> CartItems { get; set; }
    }
    sealed public class ItemQuantityMin : ValidationAttribute
    {
        public int MinQuantity;
        public override bool IsValid(object items)
        {
            bool result = true;
            List<CARTITEM> cartItems = (List<CARTITEM>) items;
            foreach (var item in cartItems)
            {
                if (item.Quantity < MinQuantity)
                {
                    result = false;
                }
            }
            // Add validation logic here.
            return result;
        }

    }
        
}
