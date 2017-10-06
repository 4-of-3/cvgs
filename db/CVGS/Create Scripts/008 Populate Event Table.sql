USE CVGS;
GO

INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate )
		   VALUES( 'Sample Event'
			     , 'A sample event for the CVGS Database'
				 , DATEFROMPARTS( 2017, 12, 20 ) );
GO

INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate
				 , ActiveStatus )
		   VALUES( 'Cancelled Event'
			     , 'An inactive sample event'
				 , DATEFROMPARTS( 2017, 12, 15 )
				 , 0 );
GO

INSERT INTO EVENT( EventTitle
				 , Description 
				 , EventDate )
		   VALUES( 'Sample Past Event'
			     , 'A sample event in the past for the CVGS Database'
				 , DATEFROMPARTS( 2017, 07, 20 ) );
GO