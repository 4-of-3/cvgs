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

SET dbname=.\SQLEXPRESS

SET pauseRequired=
SET /P pauseRequired=Pause between scripts? (Y/N) 
IF NOT '%pauseRequired%'=='' SET pauseRequired=%pauseRequired:~0,1%

ECHO .

REM============================================================
REM Create a new, empty CVGS database.  If there is an existing
REM database, all connections to it are terminated and the 
REM database is dropped.  Any non-default data in an existing
REM database will be lost.
REM============================================================
echo sqlcmd -S %dbname% -E -d master -i "000 Create Database.sql"
sqlcmd -S %dbname% -E -d master -i "000 Create Database.sql"

IF '%pauseRequired%'=='N' GOTO skip000
IF '%pauseRequired%'=='n' GOTO skip000    
    PAUSE
:skip000

REM================================================== 
REM Create the Member table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "001 Member Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "001 Member Table.sql"

IF '%pauseRequired%'=='N' GOTO skip001
IF '%pauseRequired%'=='n' GOTO skip001
    PAUSE
:skip001
REM==================================================

REM================================================== 
REM Populate the Member table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "002 Populate Member Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "002 Populate Member Table.sql"

IF '%pauseRequired%'=='N' GOTO skip002
IF '%pauseRequired%'=='n' GOTO skip002
    PAUSE
:skip002
REM==================================================
REM Create the Platform table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "003 Platform Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "003 Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip003
IF '%pauseRequired%'=='n' GOTO skip003
    PAUSE
:skip003
REM==================================================
REM Populate the Platform table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "004 Populate Platform Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "004 Populate Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip004
IF '%pauseRequired%'=='n' GOTO skip004
    PAUSE
:skip004
REM==================================================
REM Create the Game table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "005 Game Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "005 Game Table.sql"

IF '%pauseRequired%'=='N' GOTO skip005
IF '%pauseRequired%'=='n' GOTO skip005
    PAUSE
:skip005
REM==================================================
REM Populate the Game table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "006 Populate Game Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "006 Populate Game Table.sql"

IF '%pauseRequired%'=='N' GOTO skip006
IF '%pauseRequired%'=='n' GOTO skip006
    PAUSE
:skip006
REM==================================================
REM Create the Event table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "007 Event Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "007 Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip007
IF '%pauseRequired%'=='n' GOTO skip007
    PAUSE
:skip007
REM==================================================
REM Populate the Event table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "008 Populate Event Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "008 Populate Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip008
IF '%pauseRequired%'=='n' GOTO skip008
    PAUSE
:skip008
REM==================================================
REM Create the Member_Event table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "009 Member_Event Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "009 Member_Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip009
IF '%pauseRequired%'=='n' GOTO skip009
    PAUSE
:skip009
REM==================================================
REM Populate the Member_Event table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "010 Populate Member_Event Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "010 Populate Member_Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip010
IF '%pauseRequired%'=='n' GOTO skip010
    PAUSE
:skip010
REM==================================================
REM Create the Game_Platform table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "011 Game_Platform Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "011 Game_Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip011
IF '%pauseRequired%'=='n' GOTO skip011
    PAUSE
:skip011
REM==================================================
REM Populate the Game_Platform table
REM==================================================
echo sqlcmd -S %dbname% -E -d CVGS -i "012 Populate Game_Platform Table.sql"
sqlcmd -S %dbname% -E -d CVGS -i "012 Populate Game_Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip012
IF '%pauseRequired%'=='n' GOTO skip012
    PAUSE
:skip012
REM==================================================

ECHO Build.bat Finished 

IF '%pauseRequired%'=='N' GOTO skip105
IF '%pauseRequired%'=='n' GOTO skip105
    PAUSE
:skip105 

:end
REM EOF 