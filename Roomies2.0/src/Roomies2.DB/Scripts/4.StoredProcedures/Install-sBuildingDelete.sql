CREATE PROC rm2.sBuildingDelete
(
	@BuildingId INT
)

AS 
BEGIN 
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF NOT EXISTS( SELECT * FROM rm2.tBuilding b where b.BuildingId = @BuildingId)
	BEGIN
		ROLLBACK;
        RETURN 1;
    END;

	DELETE FROM rm2.itSupervisorBuilding WHERE BuildingId = @BuildingId;
	DELETE FROM rm2.itBuilding WHERE @BuildingId = @BuildingId;

	COMMIT;
	RETURN 0;
END;