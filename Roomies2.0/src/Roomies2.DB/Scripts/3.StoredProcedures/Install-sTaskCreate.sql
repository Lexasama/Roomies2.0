	CREATE PROCEDURE rm2.sTasksCreate
	(
		@TaskId     INT OUT,
		@TaskName   NVARCHAR(32),
		@TaskDes	NVARCHAR(200),
		@TaskDate	DATETIME2,
		@ColocId   INT
	)
	AS
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
		BEGIN TRAN;

		INSERT INTO rm2.tTasks ( TaskName, TaskDate, TaskDes, ColocId)
			VALUES(@TaskName, @TaskDate, @TaskDes, @ColocId);
		SET @TaskId = SCOPE_IDENTITY();
			
		COMMIT;
			RETURN 0;
	END;