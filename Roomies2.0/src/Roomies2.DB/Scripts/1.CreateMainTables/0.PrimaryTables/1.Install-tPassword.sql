CREATE TABLE RoomiesV2.rm2.tPasswordUser
(
    UserId         INT                NOT NULL,
    HashedPassword VARBINARY(128)     NOT NULL,

    CONSTRAINT PK_tPassword PRIMARY KEY (UserId),

    CONSTRAINT FK_tPasswordUser_tUser
        FOREIGN KEY (UserId)
            REFERENCES rm2.tUser (UserId)
);

INSERT INTO rm2.tPasswordUser (HashedPassword, UserId)
    VALUES (0,0);