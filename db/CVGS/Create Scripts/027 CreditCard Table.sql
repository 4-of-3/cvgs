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
           , Deleted          BIT NOT NULL DEFAULT 0
  CONSTRAINT fk_card_member FOREIGN KEY( MemberId ) REFERENCES MEMBER( MemberId )
);
GO

IF OBJECT_ID ('[tr_creditcard_soft_delete] ', 'TR') IS NOT NULL
   DROP TRIGGER [tr_creditcard_soft_delete];
GO

 CREATE TRIGGER tr_creditcard_soft_delete
     ON CREDITCARD
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;
      
     UPDATE CREDITCARD
        SET CardNumber = '****************'
          , NameOnCard = '*****'
          , CardDescription = NULL
          , ExpiryDate = CURRENT_TIMESTAMP
          , Deleted = 1
      WHERE CardId IN( SELECT CardId FROM deleted );
  END;
GO