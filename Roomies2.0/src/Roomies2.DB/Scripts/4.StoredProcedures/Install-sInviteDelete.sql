CREATE PROCEDURE rm2.sInviteDelete @ColocId INT, @Email NVARCHAR(64) AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT * FROM rm2.tInvite WHERE ColocId = @ColocId AND Email = @Email)
        BEGIN
            DELETE FROM rm2.tInvite WHERE ColocId = @ColocId AND Email = @Email;
            COMMIT;
            RETURN 0;
        END;
    ELSE
        BEGIN
            ROLLBACK;
            RETURN 1;
        END
END;