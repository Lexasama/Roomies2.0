CREATE VIEW rm2.vTasksRoomies AS
SELECT 
	t.TaskId,
	t.TaskName,
	t.TaskDate,
	t.TaskDes,
	t.State,
	r.FirstName,
	r.RoomieId,
	t.ColocId
	FROM rm2.tTasks t
		LEFT JOIN rm2.vColocMembers r ON r.ColocId = t.ColocId
	WHERE t.TaskId <> 0;
