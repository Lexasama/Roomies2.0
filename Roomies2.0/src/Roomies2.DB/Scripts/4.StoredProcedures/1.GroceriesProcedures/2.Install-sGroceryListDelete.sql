CREATE PROCEDURE rm2.sGroceryListDelete
( @GroceryListId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT * FROM rm2.tGroceryList WHERE GroceryListId = @GroceryListId)
            BEGIN
                DELETE FROM itGroceryListItem WHERE GroceryListId = @GroceryListId;
                DELETE FROM tGroceryList WHERE GroceryListId = @GroceryListId;
                COMMIT;
                RETURN 0;
            END;
        ELSE
            BEGIN
                ROLLBACK;
                RETURN 1;
            END;
END;
GO;