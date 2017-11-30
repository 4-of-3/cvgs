IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CARTITEM')
        DROP TABLE CARTITEM;
GO

CREATE TABLE CVGS.dbo.CARTITEM(
             MemberId         INT NOT NULL
           , GameId           INT NOT NULL
           , Quantity         INT NOT NULL DEFAULT 1
           , DateAdded        DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
  CONSTRAINT pk_cartitem PRIMARY KEY( MemberId, GameId ),
  CONSTRAINT fk_cartitem_member FOREIGN KEY( MemberId ) REFERENCES Member( MemberId ),
  CONSTRAINT fk_review_game FOREIGN KEY( GameId ) REFERENCES Game( GameId ) ON DELETE CASCADE
);
GO