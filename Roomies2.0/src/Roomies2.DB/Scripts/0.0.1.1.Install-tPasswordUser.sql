CREATE TABLE RoomiesV2.rm2.tPasswordUser
(
    PasswordId     INT IDENTITY(0,1) NOT NULL,
    HashedPassword VARBINARY(128) NOT NULL,

    CONSTRAINT PK_tPassword PRIMARY KEY (PasswordId),
);

INSERT INTO rm2.tPasswordUser (HashedPassword) VALUES (0);