CREATE TABLE rm2.tColoc
(
	ColocId INT IDENTITY(0,1),
	ColocName NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
	CreationDate DATETIME2 DEFAULT GETDATE(),
	PicPath NVARCHAR(255),

	CONSTRAINT PK_rm2_tColoc PRIMARY KEY (ColocId),
	CONSTRAINT CK_rm2_tColoc_ColocName CHECK(ColocName <> N'')

);

INSERT INTO rm2.tColoc(ColocName)
				VALUES(left(convert(nvarchar(36), newid()), 32));