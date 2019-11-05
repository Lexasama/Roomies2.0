create table rm2.tFacebookUser
(
	UserId			int,
	FacebookId		varchar(32) collate Latin1_General_BIN2 not null,
	RefreshToken	varchar(64) collate Latin1_General_BIN2 not null,

	constraint PK_tFacebookUser primary key(UserId),
    constraint FK_tFacebookUser_UserId foreign key(UserId) references rm2.tUser(UserId),
    constraint UK_tFacebookUser_GoogleId unique(FacebookId)
)

insert into rm2.tFacebookUser(UserId, FaceBookId, RefreshToken) values(0, 0, '');