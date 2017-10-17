IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PROVSTATE')
        DROP TABLE PROVSTATE;
GO

CREATE TABLE CVGS.dbo.PROVSTATE(
             ProvStateId      INT IDENTITY( 1,1 ) PRIMARY KEY
           , ProvStateCode    NVARCHAR(  3 ) NOT NULL
           , ProvStateName    NVARCHAR( 64 ) NOT NULL
           , CountryId        INT NOT NULL
  CONSTRAINT fk_provstate_country FOREIGN KEY( CountryId ) REFERENCES Country( CountryId ) ON DELETE CASCADE
);
GO