IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PLATFORM')
        DROP TABLE PLATFORM;
GO

CREATE TABLE CVGS.dbo.PLATFORM(
                 PlatformId INT IDENTITY(1,1) PRIMARY KEY,
              PlatformName NVARCHAR(   64 ) NOT NULL UNIQUE,
                  Developer NVARCHAR(   64 ) NOT NULL
);
GO