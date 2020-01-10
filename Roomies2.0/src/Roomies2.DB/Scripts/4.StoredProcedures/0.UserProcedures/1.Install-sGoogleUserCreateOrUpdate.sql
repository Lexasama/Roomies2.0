CREATE PROCEDURE rm2.sGoogleUserCreateOrUpdate
( 
	@Email        NVARCHAR(64),
	@GoogleId     NVARCHAR(MAX),
	@RefreshToken NVARCHAR(MAX)
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(SELECT * FROM rm2.tGoogleUser u WHERE u.GoogleId = @GoogleId)
            BEGIN
                UPDATE rm2.tGoogleUser SET RefreshToken = @RefreshToken WHERE GoogleId = @GoogleId;
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


    INSERT INTO rm2.tGoogleUser(UserId, GoogleId, RefreshToken)
						VALUES (@UserId, @GoogleId, @RefreshToken);
    COMMIT;
    RETURN 0;

END;