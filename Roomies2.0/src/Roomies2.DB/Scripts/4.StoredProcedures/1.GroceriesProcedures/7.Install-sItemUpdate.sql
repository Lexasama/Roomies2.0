CREATE PROCEDURE rm2.sItemUpdate
( @itemId    INT,
  @itemName  NVARCHAR(20),
  @unitPrice int
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    BEGIN
        IF exists(SELECT * FROM rm2.tItem WHERE ItemId = @itemId)
                BEGIN
                    UPDATE rm2.tItem
                    SET ItemName  = @itemName,
                        UnitPrice = @unitPrice
                        WHERE ItemId = @itemId;

                    COMMIT;
                    RETURN 0;
                END;
            ELSE
                BEGIN
                    ROLLBACK;
                    RETURN 1;
                END
    END;

END;
GO;