USE CVGS;
GO

IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MEMBER_EVENT')
	DROP TABLE MEMBER_EVENT;
GO

CREATE TABLE CVGS.dbo.MEMBER_EVENT(
		   MemberId        INT NOT NULL
		 , EventId         INT NOT NULL
		 , DateRegistered  DATETIME DEFAULT CURRENT_TIMESTAMP
CONSTRAINT pk_member_event PRIMARY KEY( MemberId, EventId),
CONSTRAINT fk_member_event FOREIGN KEY( MemberId ) REFERENCES MEMBER(MemberId),
CONSTRAINT fk_event_member FOREIGN KEY( EventId ) REFERENCES Event(EventId)
);
GO