CREATE PROCEDURE rm2.sFacebookUserCreateOrUpdate
( @UserId       INT,
  @FacebookId   NVARCHAR(MAX),
  @RefreshToken NVARCHAR(MAX)
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(SELECT * FROM rm2.tFacebookUser u WHERE u.FacebookId = @FacebookId)
            BEGIN
                UPDATE rm2.tFacebookUser SET RefreshToken = @RefreshToken WHERE RefreshToken = @RefreshToken;
                COMMIT;
                RETURN 0;
            END;

    INSERT INTO rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
        VALUES (@UserId, @FacebookId, @RefreshToken);
    COMMIT;
    RETURN 0;
    
END;