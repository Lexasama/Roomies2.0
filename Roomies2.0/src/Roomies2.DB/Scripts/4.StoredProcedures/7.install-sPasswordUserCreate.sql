CREATE PROCEDURE rm2.sPasswordUserCreate
(
	@Email			 NVARCHAR(64),
	@HashedPassword  VARBINARY(128),
	@UserId    INT OUT

)
AS
BEGIN
SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	IF EXISTS(SELECT * FROM rm2.tUser u WHERE u.Email = @Email)
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	INSERT INTO rm2.tUser ( Email )
                   VALUES ( @Email );

	SELECT @UserId = SCOPE_IDENTITY();

	INSERT INTO rm2.tPasswordUser(UserId, HashedPassword)
		VALUES(@UserId, @HashedPassword);


	COMMIT
	RETURN 0;

END;