CREATE TABLE tAccount
(
    AccountId INT IDENTITY(0,1) NOT NULL ,
    Hashedpassword int NOT NULL ,
    
    CONSTRAINT PK_tAccount PRIMARY KEY (AccountId),
    
)


