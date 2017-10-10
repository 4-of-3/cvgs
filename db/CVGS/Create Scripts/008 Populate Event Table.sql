INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate
				 , Location )
		   VALUES( 'Sample Event'
			     , 'A sample event for the CVGS Database'
				 , DATEFROMPARTS( 2017, 12, 20 )
				 , 'Conestoga College Doon Campus'
				 );
GO

INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate
				 , ActiveStatus 
				 , Location )
		   VALUES( 'Cancelled Event'
			     , 'An inactive sample event'
				 , DATEFROMPARTS( 2017, 12, 15 )
				 , 0
				 , 'Victoria Park'
				 );
GO

INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate 
				 , Location )
		   VALUES( 'Sample Past Event'
			     , 'A sample event in the past for the CVGS Database'
				 , DATEFROMPARTS( 2017, 07, 20 )
				 , 'Online'
				 );
GO