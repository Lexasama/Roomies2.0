CREATE VIEW [rm2].[vColocMembers]
AS
SELECT ti.RoomieId,
	   ti.ColocAdminId,
	   c.* ,
	   u.Email,
	   u.FirstName,
	   u.LastName,
	   u.Phone,
	   u.PicturePath,
	   u.Sex,
	   u.[Description],
	   u.BirthDate
	FROM rm2.itColRoom ti
		JOIN rm2.tColoc c ON c.ColocId = ti.ColocId 
		JOIN rm2.vRoomie u ON u.RoomieId = ti.RoomieId

	WHERE ti.RoomieId <> 0;
GO
