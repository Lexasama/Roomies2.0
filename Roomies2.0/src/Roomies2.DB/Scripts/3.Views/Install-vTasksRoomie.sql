CREATE VIEW rm2.vTasksRoomies AS
SELECT 
	t.TaskId,
	t.TaskName,
	t.TaskDate,
	t.TaskDes,
	t.State,
	t.ColocId
	FROM rm2.tTasks t
		LEFT JOIN rm2.vColocMembers r ON r.ColocId = t.ColocId
	WHERE t.TaskId <> 0;
