CREATE PROCEDURE sPasswordUserCreate
( @UserName       NVARCHAR(20),
  @Email          NVARCHAR(64),
  @HashedPassword VARBINARY(128),
  @FirstName      NVARCHAR(20),
  @LastName       NVARCHAR(20),
  @Phone          NVARCHAR(12),
  @Sex            INT,
  @BirthDate      DATETIME2,
  @UserId         INT OUT
)
AS
BEGIN
    -- body of the stored procedure

    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF EXISTS(
            SELECT * FROM RoomiesV2.rm2.vUser vU WHERE vU.Email LIKE @Email
        )
            BEGIN
                ROLLBACK
                RETURN 1
            END

    INSERT INTO rm2.tUser (UserName, Email, FirstName, LastName, Phone, Sex, BirthDate)
        VALUES (@UserName, @Email, @FirstName, @LastName, @Phone, @Sex, @BirthDate)

    SET @UserId = scope_identity()

    INSERT INTO rm2.tPasswordUser (UserId, HashedPassword) VALUES (@UserId, @HashedPassword)

    COMMIT
    RETURN 0;
END
GO