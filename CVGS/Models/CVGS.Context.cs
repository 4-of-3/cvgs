﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CVGSEntities : DbContext
    {
        public CVGSEntities()
            : base("name=CVGSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EVENT> EVENTs { get; set; }
        public virtual DbSet<GAME> GAMEs { get; set; }
        public virtual DbSet<MEMBER> MEMBERs { get; set; }
        public virtual DbSet<MEMBER_EVENT> MEMBER_EVENT { get; set; }
        public virtual DbSet<PLATFORM> PLATFORMs { get; set; }
        public virtual DbSet<REPORT> REPORTs { get; set; }
        public virtual DbSet<ADDRESS> ADDRESSes { get; set; }
        public virtual DbSet<ADDRESSTYPE> ADDRESSTYPEs { get; set; }
        public virtual DbSet<COUNTRY> COUNTRies { get; set; }
        public virtual DbSet<CREDITCARD> CREDITCARDs { get; set; }
        public virtual DbSet<FRIENDSHIP> FRIENDSHIPs { get; set; }
        public virtual DbSet<PROVSTATE> PROVSTATEs { get; set; }
        public virtual DbSet<REVIEW> REVIEWs { get; set; }
    
        public virtual int SP_ADD_MEMBER(string fName, string lName, string userName, string email, string pwd, string favPlatform, string favCategory, string favGame, string favQuote)
        {
            var fNameParameter = fName != null ?
                new ObjectParameter("FName", fName) :
                new ObjectParameter("FName", typeof(string));
    
            var lNameParameter = lName != null ?
                new ObjectParameter("LName", lName) :
                new ObjectParameter("LName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("pwd", pwd) :
                new ObjectParameter("pwd", typeof(string));
    
            var favPlatformParameter = favPlatform != null ?
                new ObjectParameter("FavPlatform", favPlatform) :
                new ObjectParameter("FavPlatform", typeof(string));
    
            var favCategoryParameter = favCategory != null ?
                new ObjectParameter("FavCategory", favCategory) :
                new ObjectParameter("FavCategory", typeof(string));
    
            var favGameParameter = favGame != null ?
                new ObjectParameter("FavGame", favGame) :
                new ObjectParameter("FavGame", typeof(string));
    
            var favQuoteParameter = favQuote != null ?
                new ObjectParameter("FavQuote", favQuote) :
                new ObjectParameter("FavQuote", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ADD_MEMBER", fNameParameter, lNameParameter, userNameParameter, emailParameter, pwdParameter, favPlatformParameter, favCategoryParameter, favGameParameter, favQuoteParameter);
        }
    
        public virtual int SP_MEMBER_LOGIN(string userName, string pwd, ObjectParameter memberId)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("pwd", pwd) :
                new ObjectParameter("pwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MEMBER_LOGIN", userNameParameter, pwdParameter, memberId);
        }
    }
}
