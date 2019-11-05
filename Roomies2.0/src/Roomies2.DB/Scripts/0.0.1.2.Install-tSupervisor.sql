CREATE TABLE rm2.tSupervisor
(
    SupervisorId INT NOT NULL,

    CONSTRAINT PK_tSupervisor PRIMARY KEY (SupervisorId),

    CONSTRAINT FK_tSupervisor_tUser
        FOREIGN KEY (SupervisorId)
            REFERENCES rm2.tUser (UserId)
)

INSERT INTO rm2.tSupervisor (SupervisorId)
    VALUES (0)
