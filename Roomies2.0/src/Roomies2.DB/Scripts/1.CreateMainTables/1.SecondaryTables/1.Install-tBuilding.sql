CREATE TABLE rm2.tBuilding
(
    BuildingId INT IDENTITY (0,1)                         NOT NULL,
    Adress     NVARCHAR(255) COLLATE Latin1_General_CI_AI NOT NULL,

    CONSTRAINT tBuilding_pk
        PRIMARY KEY (BuildingId)
);

INSERT INTO rm2.tBuilding (Adress)
    VALUES (N'');