CREATE VIEW rm2.vSupervisor AS
SELECT UserId,
       Email,
       FirstName,
       LastName,
       Phone
    FROM rm2.tUser                     tU
             LEFT JOIN rm2.tSupervisor tS ON tU.UserId = tS.SupervisorId
    WHERE UserId <> 0;