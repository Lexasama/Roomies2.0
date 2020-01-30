CREATE VIEW rm2.vInvite AS
	SELECT i.ColocId, 
		i.RoomieId,
		r.FirstName,
		i.Code, 
		i.Email, 
		c.ColocName	
	FROM rm2.tInvite i
		LEFT JOIN rm2.tRoomie r ON i.RoomieId = r.RoomieId
		LEFT JOIN rm2.tColoc c ON i.ColocId = c.ColocId
	Where i.RoomieId <> 0;