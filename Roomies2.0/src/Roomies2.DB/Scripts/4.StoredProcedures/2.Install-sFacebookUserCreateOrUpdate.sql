CREATE PROCEDURE rm2.sFacebookUserCreateOrUpdate
(
	@UserName	    NVARCHAR(20),
	@Email		    NVARCHAR(64),
	@FacebookId     VARCHAR(64),
	@RefreshToken   VARCHAR(200)
	
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	IF EXISTS(
		SELECT * FROM rm2.tFacebookUser u WHERE u.FacebookId = @FacebookId 
	)
	BEGIN
		UPDATE rm2.tFacebookUser SET RefreshToken = @RefreshToken WHERE FacebookId = @FacebookId;
		COMMIT;
		RETURN 0;
	END;

	DECLARE @UserId INT;
	SELECT @UserId = u.UserId FROM rm2.tUser u where u.Email = @Email;

	IF @UserId is null 
	BEGIN 
		INSERT INTO rm2.tUser(UserName, Email) VALUES (@UserName, @Email);
		SET @UserId = scope_identity();

	END;

	INSERT INTO rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
					VALUES( @UserId, @FacebookId, @RefreshToken)
	COMMIT;
	RETURN 0;
END;