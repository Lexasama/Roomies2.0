CREATE PROCEDURE rm2.sDecreaseItemAmount
( @ItemId        INT,
  @GroceryListId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF (SELECT ItemAmount FROM rm2.itGroceryListItem WHERE GroceryListId = @GroceryListId AND ItemId = @ItemId > 1)
            BEGIN
                UPDATE rm2.itGroceryListItem
                SET ItemAmount = ItemAmount - 1
                    WHERE GroceryListId = @GroceryListId
                      AND ItemId = @ItemId;
                COMMIT;
                RETURN 0;
            END;
        ELSE
            BEGIN
                EXECUTE rm2.sDeleteGroceryListItem @ItemId, @GroceryListId;
            END
END;
GO;