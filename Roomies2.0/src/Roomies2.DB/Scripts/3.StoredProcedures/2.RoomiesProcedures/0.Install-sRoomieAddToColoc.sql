CREATE PROC rm2.sRoomieAddToColoc
(
	@ColocId INT,
	@Email NVARCHAR(64)
)
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF NOT EXISTS(SELECT * FROM rm2.vRoomie r WHERE r.Email= @Email)
	BEGIN
		
		ROLLBACK;
		RETURN 1;
	END;

	DECLARE @RoomieId AS INT 
	SET @RoomieId = (SELECT RoomieId FROM rm2.vRoomie r WHERE r.Email = @Email);
	
	INSERT INTO rm2.itColRoom(RoomieId, ColocId, ColocAdminId )
	VALUES( @RoomieId, @ColocId, 0);

	COMMIT;
	RETURN 0.
	
END;