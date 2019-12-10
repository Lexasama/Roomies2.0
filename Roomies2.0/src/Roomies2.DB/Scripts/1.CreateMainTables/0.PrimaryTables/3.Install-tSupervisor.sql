CREATE TABLE rm2.tSupervisor
(
    SupervisorId INT NOT NULL,
	FirstName		NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName		NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone			NVARCHAR(12)                              NOT NULL,

    CONSTRAINT PK_tSupervisor PRIMARY KEY (SupervisorId),
    CONSTRAINT FK_tSupervisor_tUser FOREIGN KEY (SupervisorId) REFERENCES rm2.tUser (UserId)
)

INSERT INTO rm2.tSupervisor (SupervisorId, FirstName, LastName, Phone)
				     VALUES (0, N'', N'', N'');
