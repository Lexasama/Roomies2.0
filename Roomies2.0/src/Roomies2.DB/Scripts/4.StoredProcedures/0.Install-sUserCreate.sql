CREATE PROCEDURE rm2.sUserCreate
( @UserName NVARCHAR(20),
  @Email    NVARCHAR(64),
  @UserId   INT OUT
)
AS
BEGIN
    -- body of the stored procedure
    SET XACT_ABORT ON
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;
    
    IF EXISTS(
            SELECT * FROM RoomiesV2.rm2.vUser vU WHERE vU.Email LIKE @Email
        )
            BEGIN
                ROLLBACK
                RETURN 1
            END

    INSERT INTO rm2.tUser (UserName, Email, FirstName, LastName, Phone, Sex, BirthDate)
        VALUES (@UserName, @Email, N'', N'', N'', 0, N'')

    SET @UserId = scope_identity()
    COMMIT
    RETURN 0;
END
GO