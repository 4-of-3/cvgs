IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EVENT')
        DROP TABLE EVENT;
GO

CREATE TABLE CVGS.dbo.EVENT(
                   EventId         INT IDENTITY(1,1) PRIMARY KEY
                 , EventTitle      NVARCHAR(   64 ) NOT NULL
             , Description     NVARCHAR(  1024 ) NOT NULL
                 , EventDate       DATETIME NOT NULL
                 , Location                   NVARCHAR(   64 ) NOT NULL
                 , ActiveStatus    BIT NOT NULL DEFAULT 1
                 , DateCreated     DATE DEFAULT CURRENT_TIMESTAMP
);
GO