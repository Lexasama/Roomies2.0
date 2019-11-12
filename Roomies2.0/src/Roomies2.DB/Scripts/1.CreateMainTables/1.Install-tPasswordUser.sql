CREATE TABLE RoomiesV2.rm2.tPasswordUser
(
    UserId         INT                NOT NULL,
    [Password] VARBINARY(128)     NOT NULL,

    CONSTRAINT PK_tPasswordUser PRIMARY KEY (UserId),

    CONSTRAINT FK_tPasswordUser_tUser
        FOREIGN KEY (UserId)
            REFERENCES RoomiesV2.rm2.tUser (UserId)
);

INSERT INTO rm2.tPasswordUser (UserId, [Password])
    VALUES (0, convert(varbinary(128), newid()));