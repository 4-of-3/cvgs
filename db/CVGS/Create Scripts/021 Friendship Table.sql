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