CREATE PROCEDURE rm2.sGoogleUserCreateOrUpdate
( @UserId       INT,
  @GoogleId   NVARCHAR(MAX),
  @RefreshToken NVARCHAR(MAX)
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(SELECT * FROM rm2.tGoogleUser u WHERE u.GoogleId = @GoogleId)
            BEGIN
                UPDATE rm2.tGoogleUser SET RefreshToken = @RefreshToken WHERE RefreshToken = @RefreshToken;
                COMMIT;
                RETURN 0;
            END;

    INSERT INTO rm2.tGoogleUser(UserId, GoogleId, RefreshToken)
        VALUES (@UserId, @GoogleId, @RefreshToken);
    COMMIT;
    RETURN 0;

END;