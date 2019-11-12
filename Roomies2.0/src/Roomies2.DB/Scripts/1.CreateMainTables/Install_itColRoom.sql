CREATE TABLE rm2.tiColRoom
(
    ColocId    INT NOT NULL,
    RoomieId   INT NOT NULL,
    ColocAdmin INT NOT NULL,

    CONSTRAINT PK_rm2_tiColRoom
        PRIMARY KEY (ColocId, RoomieId),

    CONSTRAINT FK_rm2_tiColRoom_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId)
            ON DELETE CASCADE,

    CONSTRAINT FK_rm2_tiColRoom_tRoomie
        FOREIGN KEY (RoomieId)
            REFERENCES rm2.tRoomie (RoomieId)
            ON DELETE CASCADE
);

INSERT INTO rm2.tiColRoom(ColocId, RoomieId, ColocAdmin)
    VALUES (0, 0, 0);