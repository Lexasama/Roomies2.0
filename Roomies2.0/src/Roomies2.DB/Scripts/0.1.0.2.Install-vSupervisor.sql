CREATE VIEW rm2.vSupervisor AS
SELECT tU.UserId,
       UserName,
       Email,
       HashedPassword,
       FirstName,
       LastName,
       Phone,
       Sex,
       BirthDate
    FROM rm2.tUser                     tU
             LEFT JOIN rm2.tSupervisor tS ON tU.UserId = tS.SupervisorId
    WHERE UserId <> 0;