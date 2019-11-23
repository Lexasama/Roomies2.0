CREATE VIEW rm2.vRoomie AS
SELECT RoomieId,
       UserName,
       Email,
       FirstName,
       LastName,
       Phone,
       Sex,
       BirthDate,
       Description,
       PicturePath
    FROM tUser                 tU
             LEFT JOIN tRoomie tR ON tU.UserId = tR.RoomieId
    WHERE UserId <> 0;