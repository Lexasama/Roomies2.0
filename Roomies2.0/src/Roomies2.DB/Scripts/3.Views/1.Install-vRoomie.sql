CREATE VIEW rm2.vRoomie AS
SELECT RoomieId = u.UserId,
       UserName,
       Email,
       FirstName,
       LastName,
       Phone,
       Sex,
       BirthDate,
       Description,
       PicturePath
    FROM rm2.tUser                 u
             LEFT JOIN rm2.tRoomie r ON u.UserId = r.RoomieId
    WHERE UserId <> 0;
