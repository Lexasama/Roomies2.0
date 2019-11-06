CREATE TABLE RoomiesV2.rm2.tUser
(
    UserId    INT IDENTITY (0, 1)                       NOT NULL,

    UserName  NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    Email     NVARCHAR(64) COLLATE Latin1_General_CI_AI NOT NULL,

    FirstName NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    LastName  NVARCHAR(20) COLLATE Latin1_General_CI_AI NOT NULL,
    Phone     NVARCHAR(12)                              NOT NULL,
    Sex       INT                                       NOT NULL,
    BirthDate DATETIME2                                 NOT NULL,

    CONSTRAINT PK_tUser PRIMARY KEY (UserId),

    CONSTRAINT FK_tUser_tAccount
        FOREIGN KEY (UserId)
            REFERENCES rm2.tAccount (AccoundId),

    CONSTRAINT UK_tUser_Email UNIQUE (Email),
    CONSTRAINT UK_tUser_UserName UNIQUE (UserName),

);

INSERT INTO RoomiesV2.rm2.tUser (UserName, Email, HashedPassword, FirstName, LastName, Phone, Sex, BirthDate)
    VALUES (N'', N'', 0, N'', N'', N'', 0, N'');
