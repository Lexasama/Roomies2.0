CREATE PROCEDURE rm2.sUserUpdate
( @UserId INT, @Email NVARCHAR(64), @Email2 NVARCHAR(64)
)
AS
BEGIN
    -- body of the stored procedure
    SET XACT_ABORT ON
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(SELECT * FROM tUser WHERE Email LIKE @Email2)
            BEGIN
                ROLLBACK
                RETURN 1
            END

    IF (@Email = @Email2)
            BEGIN
                ROLLBACK
                RETURN 2
            END

    IF EXISTS(SELECT * FROM tUser WHERE Email LIKE @Email)
            UPDATE rm2.tUser SET Email = @Email2 WHERE UserId = @UserId;
    COMMIT
    RETURN 0;
END;