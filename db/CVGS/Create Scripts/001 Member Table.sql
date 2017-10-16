IF EXISTS( SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MEMBER' )
    DROP TABLE MEMBER;
GO

CREATE TABLE CVGS.dbo.MEMBER(
             MemberId      INT IDENTITY( 1,1 ) PRIMARY KEY
           , FName         NVARCHAR(  64 ) NOT NULL
           , LName         NVARCHAR(  64 ) NOT NULL
           , UserName      NVARCHAR(  25 ) NOT NULL UNIQUE
           , Email         NVARCHAR(  64 ) NOT NULL
           , Pwd           VARBINARY( 64 ) NOT NULL
           , FavPlatform   NVARCHAR(  25 )
           , FavCategory   NVARCHAR(  25 )
           , FavGame       NVARCHAR(  64 )
           , FavQuote      NVARCHAR( 140 )
           , DateJoined    DATE DEFAULT CURRENT_TIMESTAMP
           , ActiveStatus  BIT DEFAULT 1 NOT NULL
);
GO

IF OBJECT_ID( 'SP_ADD_MEMBER', 'P' ) IS NOT NULL
        DROP PROC SP_ADD_MEMBER
GO

-- Insert a new member
CREATE PROCEDURE dbo.SP_ADD_MEMBER
                 @FName       NVARCHAR(  64 )
               , @LName       NVARCHAR(  64 )
               , @UserName    NVARCHAR(  25 )
               , @Email       NVARCHAR(  64 )
               , @pwd         NVARCHAR(  64 )
               , @FavPlatform NVARCHAR(  25 )
               , @FavCategory NVARCHAR(  25 )
               , @FavGame     NVARCHAR(  64 )
               , @FavQuote    NVARCHAR( 140 )
    AS
        -- Hash the password for secure storage
        DECLARE @pwd_hash VARBINARY( 32 );
        SET @pwd_hash = HASHBYTES('SHA2_256', @pwd);

        INSERT INTO MEMBER( FName
                          , LName
                          , UserName
                          , Email
                          , pwd
                          , FavPlatform
                          , FavCategory
                          , FavGame
                          , FavQuote )
                    VALUES( @FName
                          , @LName
                          , @UserName
                          , @Email
                          , @pwd_hash
                          , @FavPlatform
                          , @FavCategory
                          , @FavGame
                          , @FavQuote );
GO

IF OBJECT_ID( 'SP_MEMBER_LOGIN', 'P' ) IS NOT NULL
        DROP PROC SP_MEMBER_LOGIN
GO

-- Returns MemberId if login is successful
-- Otherwise returns null
CREATE PROCEDURE dbo.SP_MEMBER_LOGIN
                 @UserName  NVARCHAR( 25 )
               , @pwd       NVARCHAR( 64 )
               , @MemberId  INT OUTPUT
    AS
        DECLARE @pwd_hash VARBINARY( 32 );
        SET @pwd_hash = HASHBYTES('SHA2_256', @pwd);

        -- Return the MemberId if username and password match
        SET @MemberId = ( SELECT MemberId 
                            FROM MEMBER 
                           WHERE UserName = @UserName 
                             AND pwd = @pwd_hash );
GO