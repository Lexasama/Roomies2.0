create procedure rm2.sUserUpdate
(
    @UserId int,
    @Email  nvarchar(64)
)
as
begin
    update rm2.tUser
    set Email = @Email
    where UserId = @UserId;
    return 0;
end;