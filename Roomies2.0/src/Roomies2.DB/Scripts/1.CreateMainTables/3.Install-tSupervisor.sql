CREATE TABLE rm2.tSupervisor
(
    SupervisorId INT                                       NOT NULL,
    FirstName    NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName     NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone        NVARCHAR(12)                              NOT NULL,
    Sex          INT                                       NOT NULL,
    BirthDate    DATETIME2                                 NOT NULL,

    CONSTRAINT PK_rm2_tSupervisor
        PRIMARY KEY (SupervisorId),

    CONSTRAINT FK_rm2_tSupervisor_tUser
        FOREIGN KEY (SupervisorId)
            REFERENCES rm2.tUser (UserId)
            ON DELETE CASCADE,

    CONSTRAINT CK_rm2_tSupervisor_FirstName
        CHECK (FirstName <> N''),
    CONSTRAINT CK_rm2_tSupervisor_LastName
        CHECK (LastName <> N'')

);

INSERT INTO rm2.tSupervisor (SupervisorId, FirstName, LastName, Phone, Sex, BirthDate)
    VALUES (0, left(convert(NVARCHAR(36), newid()), 32), left(convert(NVARCHAR(36), newid()), 32),
            left(convert(NVARCHAR(36), newid()), 12), 0, '00010101');
