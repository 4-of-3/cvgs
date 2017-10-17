IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'REVIEW')
        DROP TABLE REVIEW;
GO

CREATE TABLE CVGS.dbo.REVIEW(
             MemberId         INT NOT NULL
           , GameId           INT NOT NULL
           , ReviewText       NVARCHAR( 1024 )
           , Rating           INT NOT NULL
           , DateCreated      DATE NOT NULL DEFAULT CURRENT_TIMESTAMP
           , DateModified     DATE
  CONSTRAINT pk_review PRIMARY KEY( MemberId, GameId ),
  CONSTRAINT fk_review_member FOREIGN KEY( MemberId ) REFERENCES Member( MemberId ) ON DELETE CASCADE,
  CONSTRAINT fk_review_game FOREIGN KEY( GameId ) REFERENCES Game( GameId ) ON DELETE CASCADE
);
GO