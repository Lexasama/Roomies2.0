CREATE PROCEDURE rm2.sUserCreate
(
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
            SELECT * FROM RoomiesV2.rm2.tUser u WHERE u.Email = @Email
        )
            BEGIN
                ROLLBACK
                RETURN 1
            END

    INSERT INTO rm2.tUser (Email)
                   VALUES (@Email)

    SET @UserId = scope_identity()
    COMMIT
    RETURN 0;
END
GO