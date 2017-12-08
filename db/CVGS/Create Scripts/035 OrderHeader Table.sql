IF EXISTS(SELECT TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ORDERHEADER')
        DROP TABLE ORDERHEADER;
GO

CREATE TABLE CVGS.dbo.ORDERHEADER(
             OrderId            INT IDENTITY( 1,1 ) PRIMARY KEY
           , MemberId           INT NOT NULL
           , BillingAddressId   INT NOT NULL
           , ShippingAddressId  INT
           , CreditCardId       INT NOT NULL
           , DateCreated        DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
           , Processed          BIT NOT NULL DEFAULT 0
  CONSTRAINT fk_order_member FOREIGN KEY( MemberId ) REFERENCES MEMBER( MemberId ),
  CONSTRAINT fk_order_billingaddress FOREIGN KEY( BillingAddressId ) REFERENCES ADDRESS( AddressId ),
  CONSTRAINT fk_order_shippingaddress FOREIGN KEY( ShippingAddressId ) REFERENCES ADDRESS( AddressId ),
  CONSTRAINT fk_order_creditcard FOREIGN KEY( CreditCardId ) REFERENCES CREDITCARD( CardId ),
);
GO