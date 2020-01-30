CREATE PROC rm2.sColocPicUpdate
(
	@PicPath NVARCHAR(255),
	@ColocId INT
)
AS
BEGIN
	SET XACT_ABORT ON
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

	IF NOT EXISTS (SELECT PicPath FROM rm2.tColoc c WHERE c.ColocId = @ColocId)
		BEGIN 
			ROLLBACK;
			 RETURN 1;
            END;

    UPDATE rm2.tColoc
    SET
		PicPath = @PicPath
		WHERE ColocId = @ColocId;
	COMMIT;
	RETURN 0;

END;

