CREATE TABLE rm2.tRoomie
(
    RoomieId    INT NOT NULL,
	FirstName	NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName	NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone		NVARCHAR(12)                              NOT NULL,
    Sex			INT                                       NOT NULL,
    BirthDate	DATETIME2								  NOT NUll,
    Description NVARCHAR(255),
    PicturePath NVARCHAR(255),


    CONSTRAINT PK_rm2_RoomieId		PRIMARY KEY (RoomieId),
	CONSTRAINT FK_rm2_tRoomie_tUser FOREIGN KEY (RoomieId) REFERENCES rm2.tUser (UserId),
	CONSTRAINT CK_tRoomie_FirstName CHECK(FirstName <> N''),
    CONSTRAINT CK_tRoomie_LastName CHECK(LastName <> N'')

)

INSERT INTO rm2.tRoomie (RoomieId, FirstName, LastName, Phone, Sex, BirthDate, Description, PicturePath)
    VALUES (0, left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 12),0, '00010101', N'', N'');

