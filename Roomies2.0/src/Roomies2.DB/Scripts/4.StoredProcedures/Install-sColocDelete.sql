CREATE PROC rm2.sColocDelete
(
	@ColocId INT
)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	DELETE FROM rm2.itColRoom WHERE ColocId = @ColocId;
	DELETE FROM rm2.tTasks WHERE ColocId = @ColocId;
	DELETE FROM rm2.tColoc WHERE ColocId = @ColocId;
	RETURN 0;
END;