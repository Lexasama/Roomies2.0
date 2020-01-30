CREATE PROCEDURE rm2.sReccurentItemDelete
( @ItemId  INT,
  @ColocId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT *
                  FROM itRecurrentItemColoc
                  WHERE itemId = @ItemId
                    AND ColocId = @ColocId)
            BEGIN
                DELETE
                    FROM itRecurrentItemColoc
                    WHERE itemId = @ItemId
                      AND ColocId = @ColocId;
                COMMIT;
                RETURN 0;
            END;
        ELSE
            BEGIN
                ROLLBACK;
                RETURN 1;
            END;
END;
GO;