CREATE PROCEDURE rm2.sRoomieCreate
( @RoomieId    INT,
  @UserName    NVARCHAR(32),
  @FirstName   NVARCHAR(32),
  @LastName    NVARCHAR(32),
  @Phone       NVARCHAR(12),
  @Sex         INT,
  @BirthDate   DATETIME2,
  @Description NVARCHAR(225),
  @PicturePath NVARCHAR(225)
)
AS
BEGIN
    
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF EXISTS (SELECT * FROM rm2.tRoomie r WHERE r.UserName = @UserName)
	BEGIN 
		ROLLBACK;
		RETURN 1;
	END;


    INSERT INTO rm2.tRoomie(RoomieId, UserName, FirstName, LastName, Phone, Sex, BirthDate, [Description], PicturePath)
					VALUES (@RoomieId,@UserName, @FirstName, @LastName, @Phone, @Sex, @BirthDate, @Description, @PicturePath);
    COMMIT;
    RETURN 0;
END;