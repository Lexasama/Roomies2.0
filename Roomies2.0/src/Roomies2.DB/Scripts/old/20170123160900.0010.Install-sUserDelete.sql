create procedure rm2.sUserDelete
(
    @UserId int
)
as
begin
    delete from rm2.tPasswordUser where UserId = @UserId;
    delete from rm2.tGoogleUser where UserId = @UserId;
    delete from rm2.tUser where UserId = @UserId;
    return 0;
end;