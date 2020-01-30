CREATE TABLE rm2.tGroceryList
(
    GroceryListId INT IDENTITY (0,1)                        NOT NULL,
    ColocId       INT                                       NOT NULL,
    RoomieId      INT,
    ListName      NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    DueDate       DATETIME2                                 NOT NULL,

    CONSTRAINT PK_tGroceryList PRIMARY KEY (GroceryListId),

    CONSTRAINT FK_tGroceryList_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId)
            ON UPDATE CASCADE,
    CONSTRAINT FK_tGroceryList_tRoomie
        FOREIGN KEY (RoomieId)
            REFERENCES rm2.tRoomie (RoomieId)
            ON UPDATE CASCADE
);


