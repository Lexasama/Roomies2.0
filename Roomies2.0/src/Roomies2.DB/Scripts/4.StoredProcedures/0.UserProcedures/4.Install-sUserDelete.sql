CREATE PROCEDURE rm2.sUserDelete
(
    @UserId INT
)
AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT * FROM tUser WHERE UserId = @UserId)
            BEGIN
                DELETE FROM rm2.itColRoom WHERE RoomieId = @UserId;
                DELETE FROM rm2.tRoomie WHERE RoomieId = @UserId;
                DELETE FROM rm2.tUser WHERE UserId = @UserId;
                COMMIT;
                RETURN 0;
            END
        ELSE
            BEGIN
                ROLLBACK;
                RETURN 1;
            END
END;