CREATE PROC rm2.sRoomiePicUpdate
(
@PicturePath NVARCHAR(255),
@RoomieId INT
)
AS
BEGIN
	SET XACT_ABORT ON
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF NOT EXISTS (SELECT PicturePath FROM rm2.vRoomie r WHERE r.RoomieId = @RoomieId)
		BEGIN 
			ROLLBACK;
			 RETURN 1;
            END;

    UPDATE rm2.vRoomie
    SET
		PicturePath = @PicturePath
		WHERE RoomieId = @RoomieId;
	COMMIT;
	RETURN 0;

END;

