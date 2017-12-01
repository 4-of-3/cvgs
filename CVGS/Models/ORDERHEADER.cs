//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ORDERHEADER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDERHEADER()
        {
            this.ORDERITEMs = new HashSet<ORDERITEM>();
        }
    
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int BillingAddressId { get; set; }
        public Nullable<int> ShippingAddressId { get; set; }
        public int CreditCardId { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ADDRESS ADDRESS { get; set; }
        public virtual ADDRESS ADDRESS1 { get; set; }
        public virtual CREDITCARD CREDITCARD { get; set; }
        public virtual MEMBER MEMBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDERITEM> ORDERITEMs { get; set; }
    }
}
