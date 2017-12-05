IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'REVIEW')
        DROP TABLE REVIEW;
GO

CREATE TABLE CVGS.dbo.REVIEW(
             MemberId         INT NOT NULL
           , GameId           INT NOT NULL
           , ReviewText       NVARCHAR( 1024 )
           , Rating           INT NOT NULL
           , DateCreated      DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
           , DateModified     DATETIME2
           , Approved		  BIT NOT NULL DEFAULT 0
  CONSTRAINT pk_review PRIMARY KEY( MemberId, GameId ),
  CONSTRAINT fk_review_member FOREIGN KEY( MemberId ) REFERENCES Member( MemberId ),
  CONSTRAINT fk_review_game FOREIGN KEY( GameId ) REFERENCES Game( GameId )
);
GO