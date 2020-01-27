CREATE PROC rm2.sRoomieAddToColoc
(
	@ColocId INT,
	@Email NVARCHAR(64)
)
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF EXISTS(SELECT * FROM rm2.vRoomie r WHERE r.Email= @Email)
	BEGIN
		declare @RoomieId as int 
		set @RoomieId = (select RoomieId from rm2.vRoomie r WHERE r.Email = @Email);
		ROLLBACK;
		RETURN 1;
	END;

	
	
	INSERT INTO rm2.itColRoom(RoomieId, ColocId, ColocAdminId )
	VALUES( @RoomieId, @ColocId, 0);

	COMMIT;
	RETURN 0.
	
END;