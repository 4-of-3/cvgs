IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CREDITCARD')
        DROP TABLE CREDITCARD;
GO

CREATE TABLE CVGS.dbo.CREDITCARD(
             CardId           INT IDENTITY( 1,1 ) PRIMARY KEY
           , MemberId         INT NOT NULL
           , CardNumber       NVARCHAR( 64 ) NOT NULL
           , NameOnCard       NVARCHAR( 64 ) NOT NULL
		   , CardDescription  NVARCHAR( 64 )
           , ExpiryDate       DATE NOT NULL
  CONSTRAINT fk_card_member FOREIGN KEY( MemberId ) REFERENCES MEMBER( MemberId ) ON DELETE CASCADE
);
GO