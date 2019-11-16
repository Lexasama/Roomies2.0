CREATE PROCEDURE rm2.sPasswordUserCreate
(
	@UserName		 NVARCHAR(20),
	@Email			 NVARCHAR(64),
	@FirstName		 NVARCHAR(20),
	@LastName		 NVARCHAR(20),
	@Phone			 NVARCHAR(12),
    @Sex             INT,
    @BirthDate		 DATETIME2,
	@HashedPassword  VARBINARY(128),
	@UserId    INT OUT

)
AS
BEGIN
SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	IF EXISTS(SELECT * FROM rm2.vUser u WHERE u.Email LIKE @Email)
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	IF EXISTS(SELECT * FROM rm2.vUser u WHERE u.UserName LIKE @UserName)
	BEGIN
		ROLLBACK;
		RETURN 2;
	END;

	INSERT INTO rm2.tUser (UserName, Email, FirstName, LastName, Phone, Sex, BirthDate)
        VALUES (@UserName, @Email, @FirstName, @LastName, @Phone, @Sex, @BirthDate);

	SET @UserId = SCOPE_IDENTITY();

	INSERT INTO rm2.tPasswordUser(UserId, HashedPassword)
		VALUES(@UserId, @HashedPassword);


	COMMIT
	RETURN 0;

END;