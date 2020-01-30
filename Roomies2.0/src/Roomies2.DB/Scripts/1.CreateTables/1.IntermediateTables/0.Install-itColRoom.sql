CREATE TABLE rm2.itColRoom
(
    ColocId      INT NOT NULL,
    RoomieId     INT NOT NULL,
    ColocAdminId INT NOT NULL,

    CONSTRAINT PK_tiColRoom
        PRIMARY KEY (ColocId, RoomieId),

    CONSTRAINT FK_tiColRoom_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId),
    CONSTRAINT FK_tiColRoom_tRoomie
        FOREIGN KEY (RoomieId)
            REFERENCES rm2.tRoomie (RoomieId),
    CONSTRAINT FK_tiColRoom_tRoomie_ColocAdmin
        FOREIGN KEY (ColocAdminId)
            REFERENCES rm2.tRoomie (RoomieId)
);

INSERT INTO rm2.itColRoom(ColocId, RoomieId, ColocAdminId)
    VALUES (0, 0, 0);