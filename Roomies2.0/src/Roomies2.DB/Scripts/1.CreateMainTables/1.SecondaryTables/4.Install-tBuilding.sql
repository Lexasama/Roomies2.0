CREATE TABLE rm2.tBuilding
(
	BuildingId		INT IDENTITY (0, 1)                        NOT NULL,
	BuildingName	NVARCHAR(32)  COLLATE Latin1_General_CI_AI NOT NULL,
	BuildingAddress NVARCHAR(MAX) COLLATE Latin1_General_CI_AI NOT NULL

	CONSTRAINT PK_tBuilding PRIMARY KEY (BuildingId),
	CONSTRAINT FK_tBuilding_SupervisorId FOREIGN KEY (SupervisorId) REFERENCES rm2.tSupervisor(SupervisorId),
	CONSTRAINT CK_tBuilding_BuildingName CHECK(BuildingName <> N''),
	CONSTRAINT CK_tBuilding_BuildingAddress CHECK(BuildingAddress <> N''),

);

INSERT INTO rm2.tBuilding( BuildingName, BuildingAddress)
				   VALUES( left(convert(nvarchar(36), newid()), 32),left(convert(nvarchar(36), newid()), 32));