create view rm2.vUser
as
    select UserId = u.UserId,
           Email = u.Email,
           [Password] = case when p.[Password] is null then 0x else p.[Password] end,
		   FacebookRefreshToken = case when fb.RefreshToken is null then '' else fb.RefreshToken end,
		   FacebookId = case when fb.FacebookId is null then '' else fb.FacebookId end,
           GoogleRefreshToken = case when gl.RefreshToken is null then '' else gl.RefreshToken end,
           GoogleId = case when gl.GoogleId is null then '' else gl.GoogleId end
    from rm2.tUser u
        left outer join rm2.tPasswordUser p on p.UserId = u.UserId
        left outer join rm2.tGoogleUser gl on gl.UserId = u.UserId
		left outer join rm2.tFacebookUser fb on fb.UserId = u.UserId
    where u.UserId <> 0;