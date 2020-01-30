create proc rm2.sTasksUpdate
(
	@TaskId		INT, 
	@TaskName   NVARCHAR(32),
    @TaskDes	NVARCHAR(200),
	@TaskDate	DATETIME2
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	IF NOT EXISTS(SELECT * FROM rm2.tTasks t WHERE t.TaskId = @TaskId)
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	UPDATE rm2.tTasks
	SET TaskName = @TaskName, TaskDate = @TaskDate, TaskDes = @TaskDes
	WHERE TaskId = @TaskId;

	COMMIT;
    RETURN 0;
END;