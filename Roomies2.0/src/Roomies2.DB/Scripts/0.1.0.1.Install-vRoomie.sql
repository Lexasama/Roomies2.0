CREATE VIEW rm2.vRoomie AS
SELECT tU.UserId,
       UserName,
       Email,
       HashedPassword,
       FirstName,
       LastName,
       Phone,
       Sex,
       BirthDate,
       tR.Description,
       tR.PicturePath
    FROM rm2.tUser                 tU
             LEFT JOIN rm2.tRoomie tR ON tU.UserId = tR.RoomieId
    WHERE UserId <> 0;