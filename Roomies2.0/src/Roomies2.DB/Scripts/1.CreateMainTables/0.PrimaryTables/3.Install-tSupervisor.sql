CREATE TABLE rm2.tSupervisor
(
    SupervisorId	INT NOT NULL,
	FirstName		NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName		NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone			NVARCHAR(12)                              NOT NULL,

    CONSTRAINT PK_tSupervisor PRIMARY KEY (SupervisorId),
    CONSTRAINT FK_tSupervisor_tUser FOREIGN KEY (SupervisorId) REFERENCES rm2.tUser (UserId),
	CONSTRAINT UK_tSupervisor_Phone UNIQUE (Phone),
	CONSTRAINT CK_tSupervisor_FirstName CHECK(FirstName<>N''),
	CONSTRAINT CK_tSupervisor_LastName CHECK(LastName <>N''),
	CONSTRAINT CK_tSupervisor_Phone CHECK(Phone<>N'')

)

INSERT INTO rm2.tSupervisor (SupervisorId, FirstName, LastName, Phone)
				     VALUES (0, left(convert(nvarchar(36), newid()), 32), left(convert(nvarchar(36), newid()), 32), (ABS(CHECKSUM(NEWID()) % 100000000000)));
