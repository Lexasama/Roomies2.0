CREATE PROCEDURE rm2.sTaskAssign
(
	@TaskId INT OUT,
	@RoomieId INT OUT
)
AS 
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;


	IF EXISTS(SELECT * FROM rm2.tiTaskRoom t WHERE t.TaskId = @TaskId AND t.RoomieId = @RoomieId)
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	INSERT INTO rm2.tiTaskRoom(TaskId, RoomieId)
	                  VALUES(@TaskId, @RoomieId );
	COMMIT;
	RETURN 0;
END;