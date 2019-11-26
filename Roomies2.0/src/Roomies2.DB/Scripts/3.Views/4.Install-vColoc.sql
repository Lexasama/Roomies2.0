Create VIEW rm2.vCollocInfo
AS
SELECT ti.RoomieId,
	   ti.ColocAdminId,
	   c.* 
	FROM rm2.itColRoom ti
		JOIN rm2.tColoc c
			ON ti.ColocId = c.ColocId

	WHERE ti.RoomieId <> 0;