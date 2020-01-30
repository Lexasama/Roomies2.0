CREATE PROCEDURE rm2.sDeleteGroceryListItem
( @ItemId  INT,
  @GroceryListId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;
    
    DELETE FROM rm2.itGroceryListItem WHERE GroceryListId = @GroceryListId AND ItemId = @ItemId;
    COMMIT ;
    RETURN 0;
    
END;