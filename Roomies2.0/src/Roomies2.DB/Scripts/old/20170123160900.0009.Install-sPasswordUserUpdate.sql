create procedure rm2.sPasswordUserUpdate
(
    @UserId   int,
    @Password varbinary(128)
)
as
begin
    update rm2.tPasswordUser set [Password] = @Password where UserId = @UserId;
    return 0;
end;