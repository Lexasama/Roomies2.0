CREATE TABLE rm2.itGroceryListItem
(
    GroceryListId INT NOT NULL,
    ItemId        INT NOT NULL,
    ItemAmount    INT NOT NULL,

    CONSTRAINT PK_itGroceryListItem
        PRIMARY KEY (GroceryListId, ItemId),

    CONSTRAINT FK_itGroceryListItem_tGroceryList
        FOREIGN KEY (GroceryListId)
            REFERENCES rm2.tGroceryList (GroceryListId)
            ON UPDATE CASCADE,
    CONSTRAINT FK_itGroceryListItem_tItem
        FOREIGN KEY (ItemId)
            REFERENCES rm2.tItem (ItemId)
            ON UPDATE CASCADE
);