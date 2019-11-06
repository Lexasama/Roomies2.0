CREATE TABLE RoomiesV2.rm2.tGoogleUser
(
    AccountId    INT IDENTITY (0,1),
    GoogleId     VARCHAR(32) COLLATE Latin1_General_BIN2 NOT NULL,
    RefreshToken VARCHAR(64) COLLATE Latin1_General_BIN2 NOT NULL,

    CONSTRAINT PK_tGoogleUser PRIMARY KEY (AccountId),

    CONSTRAINT FK_tGoogleUser_UserId
        FOREIGN KEY (AccountId)
            REFERENCES RoomiesV2.rm2.tAccount (AccountId),

    CONSTRAINT UK_tGoogleUser_GoogleId UNIQUE (GoogleId)
);

INSERT INTO RoomiesV2.rm2.tGoogleUser(GoogleId, RefreshToken)
    VALUES (0, '');
