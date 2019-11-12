CREATE PROCEDURE rm2.sRoomieCreate
(
	@RoomieId	INT,
	@FirstName   NVARCHAR(32),
	@LastName    NVARCHAR(32),
	@Phone		 NVARCHAR(12),
	@SEX		 INT,
	@BirthDate   DATETIME2,
	@Description NVARCHAR(225),
	@PicturePath NVARCHAR(225)
)
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;
	
	INSERT INTO rm2.tRoomie( RoomieId, FirstName,LastName, Phone, Sex, BirthDate, Description, PicturePath)
					 VALUES(@RoomieId, @FirstName, @LastName, @Phone, @sex, @BirthDate, @Description, @PicturePath);
	COMMIT;
	RETURN 0;
END;