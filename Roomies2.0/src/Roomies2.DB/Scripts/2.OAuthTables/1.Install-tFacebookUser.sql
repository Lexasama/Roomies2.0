CREATE TABLE RoomiesV2.rm2.tFacebookUser
(
    UserId       INT,
    FacebookId   NVARCHAR(MAX) COLLATE Latin1_General_BIN2 NOT NULL,
    RefreshToken NVARCHAR(MAX) COLLATE Latin1_General_BIN2 NOT NULL,

    CONSTRAINT PK_tFacebookUser PRIMARY KEY (UserId),

    CONSTRAINT FK_tFacebookUser_UserId
        FOREIGN KEY (UserId)
            REFERENCES RoomiesV2.rm2.tUser (UserId),
);

INSERT INTO RoomiesV2.rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
    VALUES (0, 0, '');
