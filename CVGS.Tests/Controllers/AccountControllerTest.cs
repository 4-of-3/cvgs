﻿using CVGS.Controllers;
using CVGS.Models;
using CVGS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CVGS.Tests.Controllers
{

    [TestClass]
    public class NewAccountTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            CVGSEntities db = new CVGSEntities();
            MEMBER member;
            NewAccountViewModel account = new NewAccountViewModel()
            {
                FName="John",
                LName="Example",
                UserName="Example",
                Email="john@example.com",
                Pwd="Initial",
                FavPlatform="XBox",
                FavCategory="Adventure",
                FavGame="Super Mario Bros",
                FavQuote="It's-a me, Mario!"
            };

            // Act
            db.SP_ADD_MEMBER(account.FName, account.LName, account.UserName, account.Email, account.Pwd, account.FavPlatform, account.FavCategory, account.FavGame, account.FavQuote);

            member = db.MEMBERs.Find(db.MEMBERs.Max(m => m.MemberId));

            // Assert
            Assert.IsTrue(member.UserName == "Example"); // Username is unique, so if sp_add_member was successful and the new member has this username, the proc succeeded

            //Teardown
            db.MEMBERs.Remove(member);
            db.SaveChanges();
        }
    }
}
