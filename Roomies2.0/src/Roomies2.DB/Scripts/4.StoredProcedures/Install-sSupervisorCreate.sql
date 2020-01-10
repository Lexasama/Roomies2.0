CREATE PROC rm2.sSupervisorCreate
(
	@SupervisorId INT,
	@FirstName   NVARCHAR(32),
	@LastName    NVARCHAR(32),
	@Phone       NVARCHAR(12)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF EXISTS(SELECT * FROM rm2.tSupervisor s WHERE s.FirstName = @FirstName AND s.LastName = @LastName AND s.Phone = @Phone )
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	INSERT INTO rm2.tSupervisor(SupervisorId, FirstName, LastName, Phone)
	VALUES(@SupervisorId, @FirstName, @LastName,@Phone);

	COMMIT;
	RETURN 0.
	
END;