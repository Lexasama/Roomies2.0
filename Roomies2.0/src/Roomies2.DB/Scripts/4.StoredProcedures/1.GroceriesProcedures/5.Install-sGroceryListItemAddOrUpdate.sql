CREATE PROCEDURE rm2.sGroceryListItemAddUpdateOrDelete
( @GroceryListId INT,
  @ItemId        INT,
  @Amount        INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF (@Amount = 0)
            BEGIN
                IF exists(
                        SELECT *
                            FROM rm2.itGroceryListItem
                            WHERE GroceryListId = @GroceryListId
                              AND ItemId = @ItemId)
                        BEGIN
                            DELETE
                                FROM rm2.itGroceryListItem
                                WHERE GroceryListId = @GroceryListId
                                  AND ItemId = @ItemId;
                            COMMIT;
                            RETURN 2;
                        END;
            END;
        ELSE
            IF (@Amount > 0)
                    BEGIN
                        IF exists(
                                SELECT *
                                    FROM rm2.itGroceryListItem
                                    WHERE GroceryListId = @GroceryListId
                                      AND ItemId = @ItemId)
                                BEGIN
                                    UPDATE itGroceryListItem
                                    SET ItemAmount = @Amount
                                        WHERE GroceryListId = @GroceryListId
                                          AND ItemId = @ItemId;
                                    COMMIT;
                                    RETURN 1;
                                END;
                            ELSE
                                BEGIN
                                    INSERT INTO rm2.itGroceryListItem (GroceryListId, ItemId, ItemAmount)
                                        VALUES (@GroceryListId, @ItemId, @Amount);
                                    COMMIT;
                                    RETURN 0;
                                END;
                    END;
                ELSE
                    BEGIN
                        ROLLBACK; RETURN 3;
                    END;
END;
GO;