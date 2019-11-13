CREATE TABLE rm2.tTasks
(
    TaskId   INT IDENTITY (0,1)                        NOT NULL,
    ColocId  INT                                       NOT NULL,
    TaskName NVARCHAR(32) COLLATE Latin1_General_CI_AI NOT NULL,
    TaskDes  NVARCHAR(200) COLLATE Latin1_General_CI_AI,
    TaskDate DATETIME2,
    [State]  BIT DEFAULT 0,

    CONSTRAINT PK_rm2_tTasks PRIMARY KEY (TaskId),

    CONSTRAINT FK_rm2_tTasks_ColocId
        FOREIGN KEY (ColocId)
            REFERENCES rm2.tColoc (ColocId)
);

INSERT INTO rm2.tTasks(TaskName, TaskDate, ColocId)
    VALUES (LEFT(CONVERT(NVARCHAR(36), NEWID()), 32), '20190101', 0);