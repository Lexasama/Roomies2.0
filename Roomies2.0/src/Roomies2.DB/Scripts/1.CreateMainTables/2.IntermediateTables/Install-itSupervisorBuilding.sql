CREATE TABLE rm2.itSupervisorBuilding
(
	SupervisorId INT NOT NULL,
	BuildingId   INT NOT NULL,

	CONSTRAINT PK_rm2_itSupervisorBuilding PRIMARY KEY (SupervisorId, BuildingId),
	CONSTRAINT FK_rm2_itSupervisorBuilding_SupervisorId FOREIGN KEY (SupervisorId)
		REFERENCES rm2.tSupervisor(SupervisorId),
	CONSTRAINT FK_rm2_itSupervisorBuilding_BuildingId FOREIGN KEY( BuildingId)
		REFERENCES rm2.tBuilding(BuildingId),
);

INSERT INTO rm2.itSupervisorBuilding(SupervisorId, BuildingId)
								VALUES (0,0);