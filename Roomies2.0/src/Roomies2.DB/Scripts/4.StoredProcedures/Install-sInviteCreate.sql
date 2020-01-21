CREATE PROCEDURE rm2.sInviteCreate @ColocId INT, @RoomieId INT, @Code NVARCHAR(12), @Email NVARCHAR(64)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT * FROM rm2.tInvite WHERE ColocId = @ColocId AND Email = @Email)
        BEGIN
            ROLLBACK;
            RETURN 1;
        END;
    ELSE
        BEGIN
            INSERT INTO rm2.tInvite(ColocId, RoomieId, Code, Email) VALUES (@ColocId, @RoomieId, @Code, @Email);

            COMMIT;
            RETURN 0;
        END;
END;