IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ADDRESS')
        DROP TABLE ADDRESS;
GO

CREATE TABLE CVGS.dbo.ADDRESS(
             AddressId        INT IDENTITY( 1,1 ) PRIMARY KEY
           , MemberId         INT NOT NULL
           , StreetAddress    NVARCHAR( 64 ) NOT NULL
           , StreetAddress2   NVARCHAR( 64 )
           , City             NVARCHAR( 64 ) NOT NULL
           , PostCode         NVARCHAR( 12 ) NOT NULL
           , ProvStateId      INT
           , CountryId        INT NOT NULL
  CONSTRAINT fk_address_member FOREIGN KEY( MemberId ) REFERENCES MEMBER( MemberId ) ON DELETE CASCADE,
  CONSTRAINT fk_address_provstate FOREIGN KEY( ProvStateId ) REFERENCES PROVSTATE( ProvStateId ),
  CONSTRAINT fk_address_country FOREIGN KEY( CountryId ) REFERENCES COUNTRY( CountryId )
);
GO