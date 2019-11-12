CREATE VIEW rm2.vUser 
AS
	SELECT
			UserId =  u.UserId,
			Email = u.Email,
			UserName = u.UserName,
		    [Password] = CASE WHEN p.[Password] IS NULL THEN 0x ELSE p.[Password] END,
			
			FacebookRefreshToken = CASE WHEN f.RefreshToken IS NULL THEN '' ELSE f.RefreshToken END,
			FacebookId = CASE WHEN f.FacebookId IS NULL THEN '' ELSE f.FacebookId END,

			GoogleRefreshToken = case when gl.RefreshToken is null then '' else gl.RefreshToken end,
            GoogleId = case when gl.GoogleId is null then '' else gl.GoogleId end
    
	FROM rm2.tUser u
             LEFT JOIN rm2.tPasswordUser p  ON p.UserId  = u.UserId
             LEFT JOIN tGoogleUser		 gl ON gl.UserId = u.UserId
             LEFT JOIN tFacebookUser	 f  ON f.UserId  = u.UserId

    WHERE u.UserId <> 0;