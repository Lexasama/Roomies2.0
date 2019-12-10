CREATE TABLE rm2.itSupBuildColoc
(
    SupervisorId INT NOT NULL,
    BuildingId   INT NOT NULL,
    ColocId      INT NOT NULL,

    CONSTRAINT PK_itSupBuild PRIMARY KEY (SupervisorId, BuildingId, ColocId),

    CONSTRAINT FK_itSupBuild_tSupervisor
        FOREIGN KEY (SupervisorId)
            REFERENCES rm2.tSupervisor (SupervisorId),
    CONSTRAINT FK_itSupBuild_tBuilding
        FOREIGN KEY (BuildingId)
            REFERENCES rm2.tBuilding (BuildingId),
    CONSTRAINT FK_itSupBuild_tColoc
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId)
);

INSERT INTO rm2.itSupBuildColoc (SupervisorId, BuildingId, ColocId)
    VALUES (0, 0, 0);

