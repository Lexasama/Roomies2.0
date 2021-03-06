CREATE PROCEDURE rm2.sFacebookUserCreateOrUpdate
(
  @Email        NVARCHAR(64),
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
                UPDATE rm2.tFacebookUser SET RefreshToken = @RefreshToken WHERE @FacebookId = @FacebookId;
                COMMIT;
                RETURN 0;
            END;
	DECLARE @userId INT;
	SELECT @userId = u.UserId from rm2.tUser u where u.Email = @Email;
	
	IF @userId IS NULL
	BEGIN
		INSERT INTO rm2.tUser(Email) VALUES(@Email);
		SET @userId = SCOPE_IDENTITY();
	END;

    INSERT INTO rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
        VALUES (@userId, @FacebookId, @RefreshToken);
    COMMIT;
    RETURN 0;
    
END;