@ECHO OFF

REM Build.bat
REM
REM Use osql to run scripts that build the CVGS database in sqlexpress.
REM
REM Revision History
REM       Doug Epp, 2017.10.10: Updated to use a variable for the server instance name
REM       Doug Epp, 2017.10.03: Copied and updated for group project use
REM     James Wong, 2017.09.01: Updated for sqlcmd
REM     John McKay, 2017.08.31: Created
SET /p servername=<InstanceName.txt

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
echo sqlcmd -S %servername% -E -d master -i "000 Create Database.sql"
sqlcmd -S %servername% -E -d master -i "000 Create Database.sql"

IF '%pauseRequired%'=='N' GOTO skip000
IF '%pauseRequired%'=='n' GOTO skip000    
    PAUSE
:skip000

REM================================================== 
REM Create the Role table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "029 Role Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "029 Role Table.sql"

IF '%pauseRequired%'=='N' GOTO skip029
IF '%pauseRequired%'=='n' GOTO skip029
    PAUSE
:skip029
REM==================================================

REM================================================== 
REM Populate the Role table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "030 Populate Role Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "030 Populate Role Table.sql"

IF '%pauseRequired%'=='N' GOTO skip030
IF '%pauseRequired%'=='n' GOTO skip030
    PAUSE
:skip030
REM================================================== 
REM Create the Member table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "001 Member Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "001 Member Table.sql"

IF '%pauseRequired%'=='N' GOTO skip001
IF '%pauseRequired%'=='n' GOTO skip001
    PAUSE
:skip001
REM==================================================

REM================================================== 
REM Populate the Member table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "002 Populate Member Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "002 Populate Member Table.sql"

IF '%pauseRequired%'=='N' GOTO skip002
IF '%pauseRequired%'=='n' GOTO skip002
    PAUSE
:skip002
REM==================================================
REM Create the Platform table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "003 Platform Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "003 Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip003
IF '%pauseRequired%'=='n' GOTO skip003
    PAUSE
:skip003
REM==================================================
REM Populate the Platform table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "004 Populate Platform Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "004 Populate Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip004
IF '%pauseRequired%'=='n' GOTO skip004
    PAUSE
:skip004
REM==================================================
REM Create the Game table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "005 Game Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "005 Game Table.sql"

IF '%pauseRequired%'=='N' GOTO skip005
IF '%pauseRequired%'=='n' GOTO skip005
    PAUSE
:skip005
REM==================================================
REM Populate the Game table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "006 Populate Game Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "006 Populate Game Table.sql"

IF '%pauseRequired%'=='N' GOTO skip006
IF '%pauseRequired%'=='n' GOTO skip006
    PAUSE
:skip006
REM==================================================
REM Create the Event table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "007 Event Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "007 Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip007
IF '%pauseRequired%'=='n' GOTO skip007
    PAUSE
:skip007
REM==================================================
REM Populate the Event table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "008 Populate Event Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "008 Populate Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip008
IF '%pauseRequired%'=='n' GOTO skip008
    PAUSE
:skip008
REM==================================================
REM Create the Member_Event table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "009 Member_Event Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "009 Member_Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip009
IF '%pauseRequired%'=='n' GOTO skip009
    PAUSE
:skip009
REM==================================================
REM Populate the Member_Event table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "010 Populate Member_Event Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "010 Populate Member_Event Table.sql"

IF '%pauseRequired%'=='N' GOTO skip010
IF '%pauseRequired%'=='n' GOTO skip010
    PAUSE
:skip010
REM==================================================
REM Create the Game_Platform table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "011 Game_Platform Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "011 Game_Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip011
IF '%pauseRequired%'=='n' GOTO skip011
    PAUSE
:skip011
REM==================================================
REM Populate the Game_Platform table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "012 Populate Game_Platform Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "012 Populate Game_Platform Table.sql"

IF '%pauseRequired%'=='N' GOTO skip012
IF '%pauseRequired%'=='n' GOTO skip012
    PAUSE
:skip012
REM==================================================
REM Create the Report table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "013 Report Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "013 Report Table.sql"

IF '%pauseRequired%'=='N' GOTO skip013
IF '%pauseRequired%'=='n' GOTO skip013
    PAUSE
:skip013
REM==================================================
REM Create the Country table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "015 Country Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "015 Country Table.sql"

IF '%pauseRequired%'=='N' GOTO skip015
IF '%pauseRequired%'=='n' GOTO skip015
    PAUSE
:skip015
REM==================================================
REM Populate the Country table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "016 Populate Country Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "016 Populate Country Table.sql"

IF '%pauseRequired%'=='N' GOTO skip016
IF '%pauseRequired%'=='n' GOTO skip016
    PAUSE
:skip016
REM==================================================
REM Create the ProvState table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "017 ProvState Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "017 ProvState Table.sql"

IF '%pauseRequired%'=='N' GOTO skip017
IF '%pauseRequired%'=='n' GOTO skip017
    PAUSE
:skip017
REM==================================================
REM Populate the ProvState table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "018 Populate ProvState Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "018 Populate ProvState Table.sql"

IF '%pauseRequired%'=='N' GOTO skip018
IF '%pauseRequired%'=='n' GOTO skip018
    PAUSE
:skip018
REM==================================================
REM Create the Review table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "019 Review Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "019 Review Table.sql"

IF '%pauseRequired%'=='N' GOTO skip019
IF '%pauseRequired%'=='n' GOTO skip019
    PAUSE
:skip019
REM==================================================
REM Create the Friendship table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "021 Friendship Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "021 Friendship Table.sql"

IF '%pauseRequired%'=='N' GOTO skip021
IF '%pauseRequired%'=='n' GOTO skip021
    PAUSE
:skip021
REM==================================================
REM Populate the Friendship table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "022 Populate Friendship Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "022 Populate Friendship Table.sql"

IF '%pauseRequired%'=='N' GOTO skip022
IF '%pauseRequired%'=='n' GOTO skip022
    PAUSE
:skip022
REM==================================================
REM Create the AddressType table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "023 AddressType Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "023 AddressType Table.sql"

IF '%pauseRequired%'=='N' GOTO skip023
IF '%pauseRequired%'=='n' GOTO skip023
    PAUSE
:skip023
REM==================================================
REM Populate the AddressType table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "024 Populate AddressType Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "024 Populate AddressType Table.sql"

IF '%pauseRequired%'=='N' GOTO skip024
IF '%pauseRequired%'=='n' GOTO skip024
    PAUSE
:skip024
REM==================================================
REM Create the Address table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "025 Address Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "025 Address Table.sql"

IF '%pauseRequired%'=='N' GOTO skip025
IF '%pauseRequired%'=='n' GOTO skip025
    PAUSE
:skip025
REM==================================================
REM Create the CreditCard table
REM==================================================
echo sqlcmd -S %servername% -E -d CVGS -i "027 CreditCard Table.sql"
sqlcmd -S %servername% -E -d CVGS -i "027 CreditCard Table.sql"

IF '%pauseRequired%'=='N' GOTO skip027
IF '%pauseRequired%'=='n' GOTO skip027
    PAUSE
:skip027
REM==================================================

ECHO Build.bat Finished 

IF '%pauseRequired%'=='N' GOTO skip105
IF '%pauseRequired%'=='n' GOTO skip105
    PAUSE
:skip105 

:end
REM EOF 