CREATE TABLE rm2.tRoomie
(
    RoomieId		INT										  NOT NULL,
    UserName        NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    FirstName		NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName		NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone			NVARCHAR(12)                              NOT NULL,
    Sex				INT                                       NOT NULL,
    BirthDate		DATETIME2                                 NOT NULL,
    [Description]	NVARCHAR(255) COLLATE Latin1_General_CI_AI,
    PicturePath		NVARCHAR(255) COLLATE Latin1_General_CI_AI,


    CONSTRAINT PK_RoomieId PRIMARY KEY (RoomieId),
    CONSTRAINT FK_tRoomie_tUser FOREIGN KEY (RoomieId) REFERENCES rm2.tUser (UserId),
    CONSTRAINT UK_tRoomie_UserName UNIQUE (UserName),
	
)

INSERT INTO rm2.tRoomie (RoomieId , UserName, FirstName, LastName, Phone, Sex, BirthDate, [Description], PicturePath)
				 VALUES (0, N'', N'', N'', N'',0, '20190101', N'', N'');

