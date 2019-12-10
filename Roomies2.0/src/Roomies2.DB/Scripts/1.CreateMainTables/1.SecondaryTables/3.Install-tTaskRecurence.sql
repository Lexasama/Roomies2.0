CREATE TABLE rm2.tTaskRecurence
(
	taskRecurenceId INT IDENTITY(0,1) NOT NULL,
	TaskId			INT				  NOT NULL,
	[WeekDay]       TINYINT			  NOT NULL,
	WeekNumber		INT			      NOT NULL,
	Mounth			INT				  NOT NULL,
	[Year]			INT				  NOT NULL

	CONSTRAINT PK_rm2_tTaskRecurence PRIMARY KEY (TaskRecurenceId),
	CONSTRAINT FK_rm2_tTaskRecurence_TaskId FOREIGN KEY (TaskId) REFERENCES rm2.tTasks (TaskId)
);

INSERT INTO rm2.tTaskRecurence(TaskId, [WeekDay], WeekNumber, Mounth, [Year])
	VALUES(0, 255, 1, 1, 2019);