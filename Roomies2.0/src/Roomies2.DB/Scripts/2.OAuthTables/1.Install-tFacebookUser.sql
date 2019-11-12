CREATE TABLE RoomiesV2.rm2.tFacebookUser
(
    UserId       INT,
    FacebookId   VARCHAR(64) COLLATE Latin1_General_BIN2  NOT NULL,
    RefreshToken VARCHAR(200) COLLATE Latin1_General_BIN2 NOT NULL,

    CONSTRAINT PK_tFacebookUser PRIMARY KEY (UserId),

    CONSTRAINT FK_tFacebookUser_UserId
        FOREIGN KEY (UserId)
            REFERENCES RoomiesV2.rm2.tUser (UserId)
            ON DELETE CASCADE,

    CONSTRAINT UK_tfacebookUser_FacebookId UNIQUE (FacebookId)
);

INSERT INTO RoomiesV2.rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
    VALUES (0, 0, '');
