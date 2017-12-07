using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{

    [MetadataType(typeof(OrderHeaderMeta))]
    public partial class ORDERHEADER
    {
    }
    
        public class OrderHeaderMeta
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        [Display(Name ="Billing Address")]
        public int BillingAddressId { get; set; }
        [Display(Name = "Shipping Address")]
        public Nullable<int> ShippingAddressId { get; set; }
        [Display(Name = "Credit Card")]
        public int CreditCardId { get; set; }
        [Display(Name = "Date Created")]
        public System.DateTime DateCreated { get; set; }
        [Display(Name = "Processed")]
        public bool Processed { get; set; }
    }
}