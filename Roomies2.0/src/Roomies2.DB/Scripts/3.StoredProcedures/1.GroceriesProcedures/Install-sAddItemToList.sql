CREATE PROCEDURE rm2.sAddItemToList
(
	@ItemId INT,
	@GroceryListId INT,
	@Amount INT
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	BEGIN
		INSERT INTO rm2.itGroceryListItem(ItemId, GroceryListId, ItemAmount)
			VALUES(@ItemId, @GroceryListId, @Amount);
		COMMIT;
		RETURN 0;
	END;
END;
