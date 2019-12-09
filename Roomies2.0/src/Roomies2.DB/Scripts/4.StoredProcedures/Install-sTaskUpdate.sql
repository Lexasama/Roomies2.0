create proc rm2.sTasksUpdate
(
	@TaskId		INT, 
	@TaskName   NVARCHAR(32),
    @TaskDes	NVARCHAR(200),
	@TaskDate	DATETIME2,
	@State		BIT
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
	SET TaskName = @TaskName, TaskDate = @TaskDate, TaskDes = @TaskDes, [State] = @State
	WHERE TaskId = @TaskId;

	COMMIT;
    RETURN 0;
END;