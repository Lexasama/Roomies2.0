CREATE PROC rm2.sRoomieDelete
(
	@RoomieId INT
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

	--intemediates tables with roomie
	DELETE FROM rm2.tRoomie WHERE RoomieId = @RoomieId;  

	COMMIT;
	RETURN 0;
END;