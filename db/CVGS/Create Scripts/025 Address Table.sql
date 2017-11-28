IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ADDRESS')
        DROP TABLE ADDRESS;
GO

CREATE TABLE CVGS.dbo.ADDRESS(
             AddressId        INT IDENTITY( 1,1 ) PRIMARY KEY
           , MemberId         INT NOT NULL
           , AddressTypeId    INT NOT NULL
           , StreetAddress    NVARCHAR( 64 ) NOT NULL
           , StreetAddress2   NVARCHAR( 64 )
           , City             NVARCHAR( 64 ) NOT NULL
           , PostCode         NVARCHAR( 12 ) NOT NULL
           , ProvStateId      INT NOT NULL
           , Deleted          BIT DEFAULT 0
  CONSTRAINT fk_address_member FOREIGN KEY( MemberId ) REFERENCES MEMBER( MemberId ),
  CONSTRAINT fk_address_type FOREIGN KEY( AddressTypeId ) REFERENCES ADDRESSTYPE( AddressTypeId ), 
  CONSTRAINT fk_address_provstate FOREIGN KEY( ProvStateId ) REFERENCES PROVSTATE( ProvStateId )
);
GO

IF OBJECT_ID ('[tr_address_soft_delete] ', 'TR') IS NOT NULL
   DROP TRIGGER [tr_address_soft_delete];
GO

 CREATE TRIGGER tr_address_soft_delete
     ON ADDRESS
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;
      
     UPDATE ADDRESS
        SET StreetAddress = '*****'
          , StreetAddress2 = NULL
          , City = '*****'
          , PostCode = '******'
      WHERE AddressId IN( SELECT AddressId FROM deleted );
  END;
GO