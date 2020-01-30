CREATE TABLE RoomiesV2.rm2.tGoogleUser
(
    UserId       INT,
    GoogleId     NVARCHAR(MAX) COLLATE Latin1_General_BIN2 NOT NULL,
    RefreshToken NVARCHAR(MAX) COLLATE Latin1_General_BIN2 NOT NULL,

    CONSTRAINT PK_tGoogleUser PRIMARY KEY (UserId),
    CONSTRAINT FK_tGoogleUser_UserId FOREIGN KEY (UserId) REFERENCES RoomiesV2.rm2.tUser (UserId)
            ON UPDATE CASCADE ON DELETE CASCADE

);

INSERT INTO RoomiesV2.rm2.tGoogleUser(UserId, GoogleId, RefreshToken)
    VALUES (0, 0, '');
