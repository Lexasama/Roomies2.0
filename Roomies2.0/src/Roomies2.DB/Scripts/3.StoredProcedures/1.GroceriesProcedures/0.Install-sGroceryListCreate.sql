CREATE PROCEDURE rm2.sGroceryListCreate
( @ColocId       INT,
  @RoomieId      INT,
  @Name          NVARCHAR(20),
  @DueDate       DATETIME2,
  @GroceryListId INT OUT
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    BEGIN
        INSERT INTO rm2.tGroceryList (ColocId, RoomieId, ListName, DueDate)
            VALUES (@ColocId, @RoomieId, @Name, @DueDate);
        SET @GroceryListId = scope_identity();
        COMMIT;
        RETURN 0;
    END;
    
END;
GO;