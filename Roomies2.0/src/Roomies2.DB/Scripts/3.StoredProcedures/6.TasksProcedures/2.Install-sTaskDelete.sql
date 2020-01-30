CREATE PROCEDURE rm2.sTasksDelete
(
		@TaskId INT
)
AS
BEGIN

	DELETE FROM rm2.tiTaskRoom WHERE TaskId = @TaskId;
	DELETE FROM rm2.tTasks WHERE TaskId = @TaskId;
	RETURN 0;
END;