EXECUTE SP_ADD_MEMBER
        @FName = 'Marvin'
      , @LName= 'Aday'
      , @UserName = 'Admin'
      , @Email = 'info@cvgs.com'
      , @pwd = 'Initial'
      , @FavPlatform = ''
      , @FavCategory = ''
      , @FavGame = ''
      , @FavQuote = '';
      
EXECUTE SP_ADD_MEMBER
        @FName = 'Jane'
      , @LName= 'Example'
      , @UserName = 'jex'
      , @Email = 'jane@cvgs.com'
      , @pwd = 'Initial'
      , @FavPlatform = ''
      , @FavCategory = ''
      , @FavGame = ''
      , @FavQuote = '';

EXECUTE SP_ADD_MEMBER
        @FName = 'Ezio'
      , @LName= 'Auditore da Firenze'
      , @UserName = 'Mentore'
      , @Email = 'Ezio@assassins.com'
      , @pwd = 'Murder'
      , @FavPlatform = 'XBox'
      , @FavCategory = 'Murder'
      , @FavGame = 'Assassin''s Creed 2'
      , @FavQuote = 'We work in the darkness to serve the light';
        
EXECUTE SP_ADD_MEMBER
        @FName = 'Doug'
      , @LName= 'Epp'
      , @UserName = 'doug.epp'
      , @Email = 'doug.epp@gmail.com'
      , @pwd = 'pass123'
      , @FavPlatform = 'XBox'
      , @FavCategory = 'Adventure'
      , @FavGame = 'Assassin''s Creed: Black Flag'
      , @FavQuote = '';

EXECUTE SP_ADD_MEMBER
        @FName = 'Kendall'
      , @LName= 'Roth'
      , @UserName = 'kendall'
      , @Email = 'alwaysenough26@gmail.com'
      , @pwd = 'password'
      , @FavPlatform = 'PC'
      , @FavCategory = 'Survival'
      , @FavGame = 'Don''t Starve'
      , @FavQuote = '';
        
EXECUTE SP_ADD_MEMBER
        @FName = 'Tristan'
      , @LName= 'Freitas'
      , @UserName = 'tristan.freitas'
      , @Email = 'TFreitas@gmail.com'
      , @pwd = 'tristan'
      , @FavPlatform = 'Playstation'
      , @FavCategory = 'RPG'
      , @FavGame = 'Skyrim'
      , @FavQuote = '';
        
EXECUTE SP_ADD_MEMBER
        @FName = 'Lucas'
      , @LName= 'Benninger'
      , @UserName = 'l.b'
      , @Email = 'l.b@gmail.com'
      , @pwd = 'Initial'
      , @FavPlatform = 'Nintendo Switch'
      , @FavCategory = 'Adventure'
      , @FavGame = 'The Legend of Zelda: Breath of the Wild'
      , @FavQuote = '';
GO

UPDATE MEMBER
   SET RoleId = 2
 WHERE UserName = 'doug.epp'
    OR UserName = 'mentore'

UPDATE MEMBER
   SET RoleId = 3
 WHERE UserName = 'Admin';
GO

UPDATE MEMBER
   SET ActiveStatus = 0
 WHERE UserName = 'jex';
GO

UPDATE MEMBER
   SET DateJoined = '20170305'
 WHERE UserName = 'mentore'
    OR UserName = 'Admin';
GO