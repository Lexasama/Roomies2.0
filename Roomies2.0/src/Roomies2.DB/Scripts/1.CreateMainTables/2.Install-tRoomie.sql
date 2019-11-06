CREATE TABLE rm2.tRoomie
(
    RoomieId    INT NOT NULL,
    Description NVARCHAR(255) COLLATE Latin1_General_CI_AI,
    PicturePath NVARCHAR(255) COLLATE Latin1_General_CI_AI,


    CONSTRAINT PK_RoomieId PRIMARY KEY (RoomieId),

    CONSTRAINT FK_tRoomie_tUser
        FOREIGN KEY (RoomieId)
            REFERENCES rm2.tUser (UserId)
)

INSERT INTO rm2.tRoomie (RoomieId, Description, PicturePath)
    VALUES (0, N'', N'');

