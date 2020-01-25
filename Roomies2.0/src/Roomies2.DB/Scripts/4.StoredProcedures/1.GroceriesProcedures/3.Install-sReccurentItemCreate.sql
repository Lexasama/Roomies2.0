CREATE PROCEDURE rm2.sReccurentItemCreate
( @ItemId  INT,
  @ColocId INT
) AS
BEGIN
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    IF exists(SELECT *
                  FROM rm2.itRecurrentItemColoc
                  WHERE itemId = @ItemId
                    AND ColocId = @ColocId)
            BEGIN
                ROLLBACK;
                RETURN 1;
            END;

        ELSE
            BEGIN
                INSERT INTO rm2.itRecurrentItemColoc (ColocId, itemId)
                    VALUES (@ColocId, @ItemId);
                COMMIT;
                RETURN 0;
            END;

END;
GO;