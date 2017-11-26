 CREATE OR ALTER TRIGGER t_member_delete
     ON MEMBER
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;
      
     DELETE FROM FRIENDSHIP
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM FRIENDSHIP
      WHERE FriendId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM MEMBER_EVENT
      WHERE MemberId IN( SELECT MemberId FROM deleted );
         
     UPDATE MEMBER
        SET FName = "****"
          , LName = "****"
          , Email = "****"
          , Pwd = CAST( "****" AS VARBINARY )
          , FavPlatform = "****"
          , FavCategory = "****"
          , FavGame = "****"
          , FavQuote = "****"
          , ActiveStatus = 0
      WHERE MemberId IN( SELECT MemberId FROM deleted );
        END
GO