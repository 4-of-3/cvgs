--Members can never be fully deleted
--but should be able to remove all identifying information
IF OBJECT_ID ('[tr_delete_member_info] ', 'TR') IS NOT NULL
   DROP TRIGGER [tr_delete_member_info];
GO

 CREATE TRIGGER t_delete_member_info
     ON MEMBER
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;
      
     DELETE FROM FRIENDSHIP
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM FRIENDSHIP
      WHERE FriendId IN( SELECT MemberId FROM deleted );
         
     DELETE FROM Member_Event
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     UPDATE ADDRESS
        SET StreetAddress = '*****'
          , StreetAddress2 = NULL
          , City = '*****'
          , PostCode = '******'
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     UPDATE CREDITCARD
        SET CardNumber = '****************'
          , NameOnCard = '*****'
          , CardDescription = NULL
          , ExpiryDate = CURRENT_TIMESTAMP
          , Deleted = 1
          , CVV = '***'
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM REVIEW
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM WISHLISTITEM
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM CARTITEM
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
--     DELETE FROM ORDERHEADER
--      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     UPDATE MEMBER
        SET FName = '*****'
          , LName = '*****'
          , Email = '*****'
          , Pwd = HASHBYTES('SHA2_256', '*****')
          , FavPlatform = '*****'
          , FavCategory = '*****'
          , FavGame = '*****'
          , FavQuote = '*****'
          , DateJoined = NULL
          , ActiveStatus = 0
          , RoleId = 1
      WHERE MemberId IN( SELECT MemberId FROM deleted );
        END
GO