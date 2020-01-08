CREATE VIEW rm2.vTaskRoomies AS
SELECT 
	t.TaskId,
	r.FirstName,
	r.RoomieId,
	t.ColocId
	FROM rm2.tTasks t
		LEFT JOIN rm2.vColocMembers r ON r.ColocId = t.ColocId
	WHERE t.TaskId <> 0;
