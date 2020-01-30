CREATE TABLE rm2.tItem
(
    ItemId    INT IDENTITY (0,1)                        NOT NULL,
    ItemName  NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    UnitPrice INT DEFAULT 0,

    CONSTRAINT PK_tItem PRIMARY KEY (ItemId),
);

INSERT INTO rm2.tItem (ItemName, UnitPrice) VALUES ('dummy',0);