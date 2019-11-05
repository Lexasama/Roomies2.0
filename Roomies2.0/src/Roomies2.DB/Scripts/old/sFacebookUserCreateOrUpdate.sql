create procedure rm2.sFacebokkUserCreateOrUpdate
(
	@Email nvarchar(64),
	@FacebookId varchar(32),
	@RefreshToken varchar(64)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from rm2.tFacebookUser u where u.FacebookId = @FacebookId)
	begin
		update rm2.tFacebookUser set RefreshToken = @RefreshToken where FacebookId = @FacebookId;
		commit;
		return 0;
	end;

	declare @UserId int;
	select @userId = u.UserId from rm2.tUser u where u.Email = @Email;

	if @userId is null
	begin 
		insert into rm2.tUser(Email) values(@Email);
		set @userId = scope_identity();
	end;

	insert into rm2.tFacebookUser(UserId, FacebookId, RefreshToken)
							values(@userId, @FacebookId, @RefreshToken);

	commit; 
	return 0;

	end;