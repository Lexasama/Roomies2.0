CREATE PROCEDURE rm2.sPasswordUserCreate
(
	@UserName       NVARCHAR(20),
	@Email          NVARCHAR(64),
	@Password VARBINARY(168),
	@UserId         INT OUT
)
AS
BEGIN
    -- body of the stored procedure

    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(
            SELECT * FROM RoomiesV2.rm2.tUser u WHERE u.Email = @Email
        )
            BEGIN
                ROLLBACK
                RETURN 1
            END

	   IF EXISTS(
            SELECT * FROM RoomiesV2.rm2.tUser u WHERE u.UserName = @UserName
        )
            BEGIN
                ROLLBACK
                RETURN 2
            END

    INSERT INTO rm2.tUser (UserName, Email)
        VALUES (@UserName, @Email)

    SET @UserId = scope_identity()

    INSERT INTO rm2.tPasswordUser (UserId, [Password]) VALUES (@UserId, @Password)

    COMMIT
    RETURN 0;
END
GO