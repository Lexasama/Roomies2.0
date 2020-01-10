CREATE VIEW rm2.vTaskRoomies AS
SELECT 
t.TaskId,
i.RoomieId,
FirstName
	FROM rm2.tTasks t
	join rm2.tiTaskRoom i on i.taskId = t.TaskId
	join rm2.tRoomie r on r.RoomieId = i.RoomieId
	WHERE t.TaskId <>0