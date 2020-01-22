CREATE PROCEDURE rm2.sRoomieUpdate
( @RoomieId    INT,
  @UserName    NVARCHAR(20),
  @Email	   NVARCHAR(20),
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

    IF NOT EXISTS(SELECT * FROM rm2.tRoomie r WHERE r.RoomieId = @RoomieId)
            BEGIN
                ROLLBACK;
                RETURN 1;
            END;

    UPDATE rm2.tUser
    SET 
		
		Email     = @Email
        WHERE UserId = @RoomieId;

    UPDATE rm2.tRoomie
    SET 
		FirstName      = @FirstName,
		UserName  = @UserName,
        LastName       = @LastName,
        Phone		   = @Phone,
        Sex			   = @Sex,
        BirthDate      = @BirthDate,
		[Description]  = @Description,
        PicturePath	   = @PicturePath
        WHERE RoomieId = @RoomieId
    COMMIT;
    RETURN 0;

END;
    
    