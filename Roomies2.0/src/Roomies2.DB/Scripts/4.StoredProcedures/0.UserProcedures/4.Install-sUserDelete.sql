CREATE PROCEDURE rm2.sUserDelete
(
    @UserId INT
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    DELETE FROM rm2.tPasswordUser WHERE UserId = @UserId;
    DELETE FROM rm2.tGoogleUser   WHERE UserId = @UserId;
    DELETE FROM rm2.tFacebookUser WHERE UserId = @UserId;
    DELETE FROM rm2.tUser		  WHERE UserId = @UserId;
	COMMIT;
    RETURN 0;
END;