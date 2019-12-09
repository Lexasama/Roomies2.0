CREATE TABLE RoomiesV2.rm2.tUser
(
    UserId    INT IDENTITY (0, 1)                       NOT NULL,
	Email	  NVARCHAR(64) COLLATE Latin1_General_CI_AI NOT NULL
	

    CONSTRAINT PK_tUser PRIMARY KEY (UserId),
    CONSTRAINT UK_tUser_Email UNIQUE (Email)

);

INSERT INTO RoomiesV2.rm2.tUser ( Email )
    VALUES (N'');
