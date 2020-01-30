CREATE PROC rm2.sTasksUpdateState
(
	@TaskId	    INT
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
	SET [State] = [State]^1
	WHERE TaskId = @TaskId;

	commit;
    return 0;
end;