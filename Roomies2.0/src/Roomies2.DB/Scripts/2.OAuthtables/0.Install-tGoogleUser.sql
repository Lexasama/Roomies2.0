CREATE TABLE RoomiesV2.rm2.tGoogleUser
(
    UserId       INT,
    GoogleId     VARCHAR(32) COLLATE Latin1_General_BIN2 NOT NULL,
    RefreshToken VARCHAR(64) COLLATE Latin1_General_BIN2 NOT NULL,

    CONSTRAINT PK_tGoogleUser PRIMARY KEY (UserId),

    CONSTRAINT FK_tGoogleUser_UserId
        FOREIGN KEY (UserId)
            REFERENCES RoomiesV2.rm2.tUser (UserId),

    CONSTRAINT UK_tGoogleUser_GoogleId UNIQUE (GoogleId)
);

INSERT INTO RoomiesV2.rm2.tGoogleUser(UserId, GoogleId, RefreshToken)
    VALUES (0, 0, '');
