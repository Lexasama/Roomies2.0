CREATE PROCEDURE rm2.sItemCreate
( @Name   NVARCHAR(20),
  @Price  DECIMAL(2),
  @ItemId INT OUT
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    BEGIN
        IF exists(SELECT * FROM rm2.tItem WHERE ItemName = @Name AND UnitPrice = @Price)
                BEGIN
                    ROLLBACK;
                    RETURN 1;
                END;
            ELSE
                BEGIN
                    INSERT INTO rm2.tItem (ItemName, UnitPrice)
                        VALUES (@Name, @Price);
                    SET @ItemId = scope_identity();
                    COMMIT;
                    RETURN 0;
                END
    END;

END;
GO;