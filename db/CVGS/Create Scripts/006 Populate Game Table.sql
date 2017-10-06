USE CVGS;
GO

INSERT INTO GAME( Title
				, ISBN
				, Developer
				, Description
				, Category
				, PublicationDate
				, Cost)
         VALUES( 
		         'The Legend of Zelda: Breath of the Wild'
			   , 'B01N33O68B'
			   , 'Nintendo'
			   , 'Step into a world of discovery, exploration, and adventure in The Legend of Zelda: Breath of the Wild a boundary-breaking new game in the acclaimed series.\nTravel across vast fields, through forests, and to mountain peaks as you discover what has become of the kingdom of Hyrule In this stunning Open-Air Adventure.\nNow on Nintendo Switch, your journey is freer and more open than ever.'
			   , 'Adventure'
			   , DATEFROMPARTS(2017, 03, 03)
			   , 79.99 
			  ), ( 
			     'Halo 3'
			   , 'B000FRU0NU'
			   , 'Bungie'
			   , 'Halo 3 is the third game in the Halo Trilogy and will provide the thrilling conclusion to the events begun in Halo: Combat Evolved. Halo 3 will pick up where Halo 2 left off.'
			   , 'Shooter'
			   , DATEFROMPARTS(2007, 09, 25)
			   , 59.99), ( 
			     'Starcraft: Broodwar'
			   , 'B00005JHDQ'
			   , 'Blizzard Entertainment'
			   , 'Starcraft and Starcraft Brood War Expansion Set Starcraft the highly anticipated real-time strategy game Starcraft is the one of Blizzard Entertainment''s biggest product launch ever. '
			   , 'RTS'
			   , DATEFROMPARTS(0998, 11, 30)
			   , 19.99), ( 
			     'Grand Theft Auto V'
			   , 'B00KVSQAGO'
			   , 'Rockstar Games'
			   , 'Grand Theft Auto V for PlayStation 4, Xbox One and PC will feature a range of major visual and technical upgrades to make Los Santos and Blaine County more immersive than ever.'
			   , 'Action-adventure'
			   , DATEFROMPARTS(2014, 11, 18)
			   , 49.99), ( 
			     'Spyro the Dragon'
			   , 'B00000I1BF'
			   , 'Sony'
			   , 'Spyro the Dragon puts players in the control of the titular Spyro, as he travels across various worlds in order to rescue his fellow dragons, recover the stolen treasure, and defeat the evil Gnasty Gnorc.'
			   , 'Platformer'
			   , DATEFROMPARTS(1998, 09, 09)
			   , 59.99)
			   ;
GO