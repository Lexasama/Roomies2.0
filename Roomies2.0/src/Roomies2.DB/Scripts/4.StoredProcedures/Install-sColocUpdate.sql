CREATE PROC rm2.sCollocUpdate
(
	@ColocId   INT,
	@ColocName NVARCHAR(32), 
	@PicPath NVARCHAR(255)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
	BEGIN TRAN;

	UPDATE rm2.tColoc
	SET ColocName = @ColocName,
		PicPath = @PicPath
	WHERE ColocId = @ColocId;

	COMMIT;
    RETURN 0;
END;