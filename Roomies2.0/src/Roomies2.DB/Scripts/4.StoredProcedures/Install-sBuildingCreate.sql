CREATE PROC rm2.sBuidingCreate
(
	@BuildingName	 NVARCHAR(32), 
	@BuildingAddress NVARCHAR(MAX),
	@BuildingId		 INT OUT      
)
AS
BEGIN

	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF EXISTS( SELECT * FROM rm2.tBuilding b WHERE b.BuildingName = @BuildingName AND b.BuildingAddress = @BuildingAddress )

		BEGIN
                ROLLBACK;
                RETURN 1;
        END
	INSERT INTO rm2.tBuilding(BuildingName, BuildingAddress )
						VALUES( @BuildingName, @BuildingAddress)

	SET @BuildingId = SCOPE_IDENTITY()
	COMMIT
	RETURN 0;
END;