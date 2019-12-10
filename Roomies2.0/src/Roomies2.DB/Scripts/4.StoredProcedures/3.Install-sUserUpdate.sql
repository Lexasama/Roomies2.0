CREATE PROCEDURE rm2.sUserUpdate
( 
	@UserId INT,
	@Email NVARCHAR(64)

)
AS
BEGIN
    -- body of the stored procedure
    SET XACT_ABORT ON
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

		UPDATE rm2.tUser 
			SET Email = @Email
			WHERE UserId = @UserId;
    COMMIT
    RETURN 0;
END;