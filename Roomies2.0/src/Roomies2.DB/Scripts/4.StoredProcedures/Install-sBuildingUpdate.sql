CREATE PROC rm2.tBuildingUpdate
(
	@BuildingId INT,
	@BuildingName NVARCHAR(32)
)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	IF NOT EXISTS(SELECT * FROM rm2.tBuilding b WHERE b.BuildingId = @BuildingId);
	BEGIN
		ROLLBACK;
		RETURN 1;
	END;

	UPDATE rm2.tBuilding SET BuildingName = @BuildingId
						WHERE BuildingId = @BuildingId;
	COMMIT;
	RETURN 0;
END;