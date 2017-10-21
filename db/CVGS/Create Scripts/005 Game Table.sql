IF EXISTS( SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GAME' )
        DROP TABLE GAME;
GO

CREATE TABLE CVGS.dbo.GAME(
             GameId             INT IDENTITY( 1,1 ) PRIMARY KEY
           , Title              NVARCHAR(   64 ) NOT NULL
           , ISBN               NVARCHAR(   10 ) NOT NULL
           , Developer          NVARCHAR(   64 ) NOT NULL
           , Description        NVARCHAR( 1024 ) NOT NULL
           , Category           NVARCHAR(   64 ) NOT NULL
           , ImageUrl           NVARCHAR( 1024 )
           , PublicationDate    Date NOT NULL
           , Cost               MONEY NOT NULL
           , Digital            BIT NOT NULL DEFAULT 0
           , Deleted            BIT NOT NULL DEFAULT 0
);