CREATE PROCEDURE rm2.sRoomieCreate
( @RoomieId    INT,
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

    UPDATE tUser SET 
                     FirstName = @FirstName, 
                     LastName = @LastName, 
                     tUser.Phone = @Phone,
                     Sex = @Sex,
                     BirthDate = @BirthDate
    WHERE UserId = @RoomieId;

    INSERT INTO rm2.tRoomie(RoomieId, Description, PicturePath)
        VALUES (@RoomieId, @Description, @PicturePath);
    COMMIT;
    RETURN 0;
END;