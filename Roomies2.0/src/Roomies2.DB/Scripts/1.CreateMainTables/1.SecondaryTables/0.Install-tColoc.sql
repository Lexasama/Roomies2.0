CREATE TABLE rm2.tColoc
(
    ColocId      INT IDENTITY (0,1),
    ColocName    NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    CreationDate DATETIME2 DEFAULT GETDATE(),
    PicPath      NVARCHAR(255)                             ,

    CONSTRAINT PK_rm2_tColoc
        PRIMARY KEY (ColocId)
);

INSERT INTO rm2.tColoc(ColocName, PicPath)
    VALUES (left(convert(NVARCHAR(36), newid()), 32), 'http://localhost:5000/Default/favicon.png');