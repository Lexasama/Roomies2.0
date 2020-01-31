CREATE TABLE rm2.tInvite
(
    ColocId  INT          NOT NULL,
    RoomieId INT          NOT NULL,
    Code     NVARCHAR(12) NOT NULL,
    Email    NVARCHAR(64) COLLATE Latin1_General_CI_AI NOT NULL ,

    CONSTRAINT PK_tInvite PRIMARY KEY (ColocId, RoomieId),
	CONSTRAINT UK_tInvite UNIQUE(Code, Email),
    CONSTRAINT FK_tInvite_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId),
    CONSTRAINT FK_tInvite_tRoomie
        FOREIGN KEY (RoomieId)
            REFERENCES rm2.tRoomie (RoomieId)
);
