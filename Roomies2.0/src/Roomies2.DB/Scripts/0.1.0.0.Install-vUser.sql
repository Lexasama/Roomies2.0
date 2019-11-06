CREATE VIEW rm2.vUser AS
SELECT tU.UserId,
       UserName,
       Email,
       HashedPassword,
       FirstName,
       LastName,
       Phone,
       Sex,
       BirthDate
    FROM rm2.tUser tU
    WHERE UserId <> 0;