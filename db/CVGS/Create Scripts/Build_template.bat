@ECHO OFF

REM Build.bat
REM
REM Use osql to run scripts that build the CVGS database in sqlexpress.
REM
REM Revision History
REM     James Wong, 2017.09.01: Updated for sqlcmd
REM     John McKay, 2017.08.31: Created
REM     John McKay, 2012.02.03: Disabled call to "025 Build Assembly.bat".
REM         To restore: 
REM         - Edit "025 Build Assembly.bat" to ensure that the
REM           C# compiler (csc.exe) for the desired .NET version
REM           can be executed successfuly
REM         - Search for "REM ***" and remove the REM that prevents 
REM           "025 Build Assembly.bat" from executing

SET pauseRequired=
SET /P pauseRequired=Pause between scripts? (Y/N) 
IF NOT '%pauseRequired%'=='' SET pauseRequired=%pauseRequired:~0,1%

ECHO .

SET fillPopulation=
SET /P fillPopulation=Fill the population table? (Y/N) 
IF NOT '%fillPopulation%'=='' SET fillPopulation=%fillPopulation:~0,1%

ECHO .

REM============================================================
REM Create a new, empty CVGS database.  If there is an existing
REM database, all connections to it are terminated and the 
REM database is dropped.  Any non-default data in an existing
REM database will be lost.
REM============================================================
sqlcmd -S .\SQLEXPRESS -E -d master -i "000 Create Database.sql"

IF '%pauseRequired%'=='N' GOTO skip000
IF '%pauseRequired%'=='n' GOTO skip000    
    PAUSE
:skip000

REM================================================== 
REM Create User Defined Data types, which are used to
REM define all columns in the CVGS database.
REM==================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "005 Create UDTs.sql"

IF '%pauseRequired%'=='N' GOTO skip005
IF '%pauseRequired%'=='n' GOTO skip005
    PAUSE
:skip005

REM=====================================================
REM Create the EventLog table and leave it empty.
REM Create lookup tables and use BULK INSERT to populate
REM them with default data. With BULK INSERT triggers
REM are not fired.
REM
REM Maintenance note:  There are hard-coded directories
REM in ..\Default Data\*.txt files.
REM=====================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "010 Create Log and Lookups.sql"

IF '%pauseRequired%'=='N' GOTO skip010
IF '%pauseRequired%'=='n' GOTO skip010
    PAUSE
:skip010

REM==================================================
REM Create the data tables, including constraints and
REM indexes, and leave them empty.
REM==================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "015 Create Data Tables.sql"

IF '%pauseRequired%'=='N' GOTO skip015
IF '%pauseRequired%'=='n' GOTO skip015
    PAUSE
:skip015

REM============================================
REM Create GameCompany T-SQL stored procedures.
REM============================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "016 Create GameCompany T-SQL Stored Procedures.sql"

IF '%pauseRequired%'=='N' GOTO skip016
IF '%pauseRequired%'=='n' GOTO skip016
    PAUSE
:skip016

REM==========================================
REM Create Inventory T-SQL stored procedures.
REM==========================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "017 Create Inventory T-SQL Stored Procedures.sql"

IF '%pauseRequired%'=='N' GOTO skip017
IF '%pauseRequired%'=='n' GOTO skip017
    PAUSE
:skip017

REM=======================================================
REM Drop the SqlServerCode assembly from the CVGS database.
REM=======================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "020 Drop Assembly.sql"

IF '%pauseRequired%'=='N' GOTO skip020
IF '%pauseRequired%'=='n' GOTO skip020
    PAUSE
:skip020

REM====================================================
REM Build the SqlServerCode project using the csc
REM command-line compiler.
REM
REM Maintenance note:  There are hard-coded directories
REM in BuildAssembly.bat
REM====================================================

REM ***
REM CALL "025 Build Assembly.bat"
REM ***

REM IF '%pauseRequired%'=='N' GOTO skip025
REM IF '%pauseRequired%'=='n' GOTO skip025
REM    PAUSE
REM :skip025

REM==============================================
REM Deploy SqlServerCode.dll to the CVGS database.
REM If deployment fails, the remaining scripts
REM will also fail.
REM
REM Maintenance note:  Each item in SqlServerData 
REM (stored procedure, trigger, etc.) must be 
REM individually deployed.
REM==============================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "030 Deploy Assembly.sql"

IF '%pauseRequired%'=='N' GOTO skip030
IF '%pauseRequired%'=='n' GOTO skip030
    PAUSE
:skip030

REM=============================================
REM Insert default data into the Location table.
REM=============================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "035 Fill Location.sql"

IF '%pauseRequired%'=='N' GOTO skip035
IF '%pauseRequired%'=='n' GOTO skip035
    PAUSE
:skip035 

REM=========================================================
REM Insert default data into the Person and Employee tables.
REM=========================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "040 Fill Employee.sql"

IF '%pauseRequired%'=='N' GOTO skip040
IF '%pauseRequired%'=='n' GOTO skip040
    PAUSE
:skip040

REM======================================================
REM Insert default data into the Game and Product tables.
REM======================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "045 Fill Game.sql"

IF '%pauseRequired%'=='N' GOTO skip045
IF '%pauseRequired%'=='n' GOTO skip045
    PAUSE
:skip045

REM==================================================================
REM Insert default data into the Supplier and SupplierContact tables.
REM==================================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "050 Fill Supplier.sql"

IF '%pauseRequired%'=='N' GOTO skip050
IF '%pauseRequired%'=='n' GOTO skip050
    PAUSE
:skip050

IF '%fillPopulation%'=='N' GOTO skipPopulation
IF '%fillPopulation%'=='n' GOTO skipPopulation   

REM=====================================================
REM Bulk load the Population table. 
REM
REM 500,000 records are loaded in 10,000 record batches.
REM=====================================================
sqlcmd -S .\SQLEXPRESS -E -d CVGS -i "100 Fill Population.sql"

IF '%pauseRequired%'=='N' GOTO skip100
IF '%pauseRequired%'=='n' GOTO skip100
    PAUSE
:skip100 

:skipPopulation

ECHO Build.bat Finished 

IF '%pauseRequired%'=='N' GOTO skip105
IF '%pauseRequired%'=='n' GOTO skip105
    PAUSE
:skip105 

:end
REM EOF 