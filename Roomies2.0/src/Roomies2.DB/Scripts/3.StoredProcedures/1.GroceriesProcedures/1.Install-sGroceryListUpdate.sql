CREATE PROCEDURE rm2.sGroceryListUpdate
( @GroceryListId INT,
  @RoomieId      INT,
  @Name          NVARCHAR(20),
  @DueDate       DATETIME2
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT *
                  FROM RoomiesV2.rm2.tGroceryList
                  WHERE GroceryListId = @GroceryListId)
            BEGIN
                UPDATE rm2.tGroceryList
                SET RoomieId = @RoomieId,
                    ListName = @Name,
                    DueDate  = @DueDate
                    WHERE GroceryListId = @GroceryListId;
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