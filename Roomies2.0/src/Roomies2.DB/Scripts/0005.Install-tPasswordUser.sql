create table RoomiesV2.rm2.tPasswordUser
(
    UserId     int,
    [Password] varbinary(128) not null,

    constraint PK_tPasswordUser primary key(UserId),
    constraint FK_tPasswordUser_UserId foreign key(UserId) references RoomiesV2.rm2.tUser(UserId)
);

insert into RoomiesV2.rm2.tPasswordUser(UserId, [Password]) values(0, convert(varbinary(128), newid()));