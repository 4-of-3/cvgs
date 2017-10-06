
USE master;

GO

IF EXISTS (SELECT Name
           FROM Sysdatabases
           WHERE Name = 'CVGS')
    BEGIN
        PRINT 'The existing CVGS database will be dropped.';
 
        ALTER DATABASE CVGS 
            SET SINGLE_USER 
            WITH ROLLBACK IMMEDIATE;
    
        DROP DATABASE CVGS;
    END

PRINT 'CREATE DATABASE CVGS;'

CREATE DATABASE CVGS;

GO

USE CVGS;

GO