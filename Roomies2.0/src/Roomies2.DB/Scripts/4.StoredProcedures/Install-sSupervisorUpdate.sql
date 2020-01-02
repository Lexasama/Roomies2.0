CREATE PROC rm2.sSupervisorUpdate
(
	@SupervisorId INT,
	@Email		  NVARCHAR(64),
	@FirstName	  NVARCHAR(32),
	@LastName	  NVARCHAR(32),
	@Phone		  NVARCHAR(32)

)
AS
BEGIN

	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF NOT EXISTS( SELECT * FROM rm2.tSupervisor s WHERE s.SupervisorId = @SupervisorId);
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;
	
	UPDATE rm2.tSupervisor SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone 
		WHERE SupervisorId = @SupervisorId;	
	UPDATE rm2.tUser SET Email = @Email Where UserId = @SupervisorId;

	COMMIT;
	RETURN 0;

END;