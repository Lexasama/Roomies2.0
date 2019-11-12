CREATE PROCEDURE  rm2.sGoogleUserCreateOrUpdate
(
	@UserName	  NVARCHAR(20),
    @Email        nvarchar(64),
    @GoogleId     varchar(32),
    @RefreshToken varchar(64)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	if exists(SELECT * FROM rm2.tGoogleUser u WHERE u.GoogleId = @GoogleId)
	begin
		update rm2.tGoogleUser set RefreshToken = @RefreshToken where GoogleId = @GoogleId;
		commit;
		return 0;
	end;

    DECLARE @UserId INT;
	SELECT @UserId = u.UserId FROM rm2.tUser u WHERE u.Email = @Email;

	IF @UserId is null
	BEGIN
		INSERT INTO rm2.tUser(UserName, Email) VALUES(@UserName, @Email);
		SET @UserId = scope_identity();
	END;
    
    INSERT INTO rm2.tGoogleUser(UserId,  GoogleId,  RefreshToken)
                         VALUES(@UserId, @GoogleId, @RefreshToken);
	COMMIT;
    RETURN 0;
END;