INSERT INTO EVENT( EventTitle
                 , Description 
                 , EventDate
                 , Location )
           VALUES( 'Sample Event'
                 , 'A sample event for the CVGS Database'
                 , '20171220 10:30:00 AM'
                 , 'Conestoga College Doon Campus'
);
GO

INSERT INTO EVENT( EventTitle
                 , Description 
                 , EventDate 
                 , Location )
           VALUES( 'Sample Past Event'
                 , 'A sample event in the past for the CVGS Database'
                 , '20170720'
                 , 'Online'
);
GO

INSERT INTO EVENT( EventTitle
                 , Description 
                 , EventDate
                 , ActiveStatus 
                 , Location )
           VALUES( 'Cancelled Event'
                 , 'An inactive sample event'
                 , '20171215 4:00:00 PM'
                 , 0
                 , 'Victoria Park'
);
GO