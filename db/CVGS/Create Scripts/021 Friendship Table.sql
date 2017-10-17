IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FRIENDSHIP')
        DROP TABLE FRIENDSHIP;
GO

CREATE TABLE CVGS.dbo.FRIENDSHIP(
             Member1Id        INT NOT NULL
           , Member2Id        INT NOT NULL
           , DateCreated      DATE NOT NULL DEFAULT CURRENT_TIMESTAMP
  CONSTRAINT pk_friendship PRIMARY KEY( Member1Id, Member2Id ),
  CONSTRAINT fk_friendship_member1 FOREIGN KEY( Member1Id ) REFERENCES Member( MemberId ),
  CONSTRAINT fk_friendship_member2 FOREIGN KEY( Member2Id ) REFERENCES Member( MemberId )
);
GO