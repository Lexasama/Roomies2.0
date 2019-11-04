create procedure rm2.sPasswordUserCreate
(
    @Email    nvarchar(64),
    @Password varbinary(128),
	@UserId   int out
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from rm2.tUser u where u.Email = @Email)
	begin
		rollback;
		return 1;
	end;

    insert into rm2.tUser(Email) values(@Email);
    select @UserId = scope_identity();
    insert into rm2.tPasswordUser(UserId,  [Password])
                           values(@UserId, @Password);
	commit;
    return 0;
end;
