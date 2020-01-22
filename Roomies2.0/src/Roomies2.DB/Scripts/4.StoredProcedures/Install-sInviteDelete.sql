CREATE PROCEDURE rm2.sInviteDelete 
(
	@ColocId INT,
	@Email NVARCHAR(64) 
)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF NOT EXISTS(SELECT * FROM rm2.tInvite WHERE ColocId = @ColocId AND Email = @Email)
        BEGIN
           ROLLBACK;
           RETURN 1;
        END;
	
  DELETE FROM rm2.tInvite WHERE ColocId = @ColocId AND Email = @Email;
            COMMIT;
			RETURN 0;
END;
