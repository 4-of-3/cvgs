IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ORDERITEM')
        DROP TABLE ORDERITEM;
GO

CREATE TABLE CVGS.dbo.ORDERITEM(
             OrderId         INT NOT NULL
           , GameId          INT NOT NULL
           , Quantity        INT NOT NULL DEFAULT 1
  CONSTRAINT pk_orderitem PRIMARY KEY( OrderId, GameId ),
  CONSTRAINT fk_orderitem_order FOREIGN KEY( OrderId ) REFERENCES OrderHeader( OrderId ),
  CONSTRAINT fk_orderitem_game FOREIGN KEY( GameId ) REFERENCES Game( GameId ) ON DELETE CASCADE
);
GO