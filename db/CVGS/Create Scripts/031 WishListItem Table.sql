IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WISHLISTITEM')
        DROP TABLE WISHLISTITEM;
GO

CREATE TABLE CVGS.dbo.WISHLISTITEM(
             MemberId         INT NOT NULL
           , GameId           INT NOT NULL
           , DateAdded        DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
  CONSTRAINT pk_listitem PRIMARY KEY( MemberId, GameId ),
  CONSTRAINT fk_wishlist_member FOREIGN KEY( MemberId ) REFERENCES Member( MemberId ),
  CONSTRAINT fk_wishlist_game FOREIGN KEY( GameId ) REFERENCES Game( GameId ) ON DELETE CASCADE
);
GO