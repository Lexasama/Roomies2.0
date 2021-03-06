CREATE PROCEDURE rm2.sColocCreate
(
	@ColocName   NVARCHAR(32),
	@RoomieId    INT,
	@ColocId	 INT OUT 
)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;
	
	INSERT INTO rm2.tColoc  ( ColocName, PicPath )
	                  values( @ColocName,'http://localhost:5000/Default/favicon.png' )
	set @ColocId = SCOPE_IDENTITY();
	insert into rm2.itColRoom(ColocId, RoomieId, ColocAdminId)
		values(@ColocId, @RoomieId, 1);

	commit;
	return 0;
end;