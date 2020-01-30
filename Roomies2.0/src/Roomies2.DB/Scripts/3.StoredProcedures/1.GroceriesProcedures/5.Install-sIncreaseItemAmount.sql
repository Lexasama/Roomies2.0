CREATE PROCEDURE rm2.sIncreaseItemAmount
( @ItemId        INT,
  @GroceryListId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT * FROM rm2.itGroceryListItem WHERE GroceryListId = @GroceryListId AND ItemId = @ItemId)
            BEGIN
                UPDATE rm2.itGroceryListItem
                SET ItemAmount = ItemAmount + 1
                    WHERE GroceryListId = @GroceryListId
                      AND ItemId = @ItemId;
                COMMIT;
                RETURN 0;
            END;
        ELSE
            BEGIN
                INSERT INTO rm2.itGroceryListItem (GroceryListId, ItemId, ItemAmount)
                    VALUES (@GroceryListId, @ItemId, 1);
                COMMIT ;
                RETURN 1;
            END;
END;