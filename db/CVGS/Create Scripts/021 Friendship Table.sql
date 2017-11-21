IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FRIENDSHIP')
        DROP TABLE FRIENDSHIP;
GO

CREATE TABLE CVGS.dbo.FRIENDSHIP(
             MemberId         INT NOT NULL
           , FriendId         INT NOT NULL
           , DateCreated      DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
  CONSTRAINT pk_friendship PRIMARY KEY( MemberId, FriendId ),
  CONSTRAINT ak_friendship UNIQUE( MemberId, FriendId ), 
  CONSTRAINT fk_friendship_member FOREIGN KEY( MemberId ) REFERENCES Member( MemberId ),
  CONSTRAINT fk_friendship_friend FOREIGN KEY( FriendId ) REFERENCES Member( MemberId )
);
GO


-- Because we can't use ON CASCADE DELETE on the FRIENDSHIP table,
-- create a trigger to emulate the same behaviour
 CREATE OR ALTER TRIGGER friendship_cascade_delete
     ON MEMBER
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;
      
     DELETE FROM FRIENDSHIP
      WHERE MemberId IN( SELECT MemberId FROM deleted );
      
     DELETE FROM FRIENDSHIP
      WHERE FriendId IN( SELECT MemberId FROM deleted );
         
     DELETE FROM MEMBER
      WHERE MemberId IN( SELECT MemberId FROM deleted );
        END
        
GO