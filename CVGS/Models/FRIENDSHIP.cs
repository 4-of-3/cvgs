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
    
    public partial class FRIENDSHIP
    {
        public System.DateTime DateCreated { get; set; }
        public int MemberId { get; set; }
        public int FriendId { get; set; }
    
        public virtual MEMBER FRIEND { get; set; }
        public virtual MEMBER MEMBER { get; set; }
    }
}
