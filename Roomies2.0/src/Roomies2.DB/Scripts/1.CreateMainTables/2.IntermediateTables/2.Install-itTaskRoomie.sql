CREATE TABLE rm2.tiTaskRoom
(
    TaskId   INT NOT NULL,
    RoomieId INT NOT NULL,

    CONSTRAINT PK_rm2_tiTaskRoom 
        PRIMARY KEY (TaskId, RoomieId),
        
    CONSTRAINT FK_rm2_tiTaskRoom_RoomieId 
        FOREIGN KEY (RoomieId) 
            REFERENCES rm2.tRoomie (RoomieId),
    CONSTRAINT FK_rm2_tiTaskRoom_TaskId 
        FOREIGN KEY (TaskId) 
            REFERENCES rm2.tTasks (TaskId)
);

INSERT INTO rm2.tiTaskRoom(TaskId, RoomieId)
    VALUES (0, 0);