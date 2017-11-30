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
           , PublicationDate    DATE NOT NULL
           , Cost               MONEY NOT NULL
           , Digital            BIT NOT NULL DEFAULT 0
           , Deleted            BIT NOT NULL DEFAULT 0
);

IF OBJECT_ID ('[tr_game_soft_delete] ', 'TR') IS NOT NULL
   DROP TRIGGER [tr_game_soft_delete];
GO

 CREATE TRIGGER tr_game_soft_delete
     ON GAME
INSTEAD OF DELETE
     AS
      BEGIN
        SET NOCOUNT ON;

--     DELETE FROM REVIEW
--      WHERE GameId IN SELECT( GameId FROM deleted );
      
     UPDATE GAME
        SET Deleted = 1
      WHERE GameId IN( SELECT GameId FROM deleted );
  END;
GO