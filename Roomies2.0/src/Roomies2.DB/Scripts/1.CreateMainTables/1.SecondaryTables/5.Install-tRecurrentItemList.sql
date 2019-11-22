CREATE TABLE rm2.itRecurrentItemColoc
(
    ColocId INT NOT NULL,
    itemId  INT NOT NULL,

    CONSTRAINT PK_tRecurrentItemList PRIMARY KEY (ColocId, itemId),

    CONSTRAINT FK_tRecurrentItemList_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId)
            ON UPDATE CASCADE,
    CONSTRAINT FK_tRecurrentItemList_tItem
        FOREIGN KEY (itemId)
            REFERENCES rm2.tItem (ItemId)
            ON UPDATE CASCADE
);