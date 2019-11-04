create procedure rm2.sGithubUserCreateOrUpdate
(
    @Email       nvarchar(64),
    @GithubId    int,
    @AccessToken varchar(64)
)
as
begin
	set transaction isolation level serializable;
	begin tran;

	if exists(select * from rm2.tGithubUser u where u.GithubId = @GithubId)
	begin
		update rm2.tGithubUser set AccessToken = @AccessToken where GithubId = @GithubId;
		commit;
		return 0;
	end;

    declare @userId int;
	select @userId = u.UserId from rm2.tUser u where u.Email = @Email;

	if @userId is null
	begin
		insert into rm2.tUser(Email) values(@Email);
		set @userId = scope_identity();
	end;
    
    insert into rm2.tGithubUser(UserId,  GithubId,  AccessToken)
                         values(@userId, @GithubId, @AccessToken);
	commit;
    return 0;
end;