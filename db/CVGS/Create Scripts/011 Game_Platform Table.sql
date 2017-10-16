IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GAME_PLATFORM')
        DROP TABLE GAME_PLATFORM;
GO

CREATE TABLE CVGS.dbo.GAME_PLATFORM(
                   GameId        INT NOT NULL
                 , PlatformId         INT NOT NULL
CONSTRAINT pk_game_platform PRIMARY KEY( GameId, PlatformId),
CONSTRAINT fk_game_platform FOREIGN KEY( GameId ) REFERENCES Game(GameId) ON DELETE CASCADE,
CONSTRAINT fk_platform_game FOREIGN KEY( PlatformId ) REFERENCES Platform(PlatformId) ON DELETE CASCADE
);
GO