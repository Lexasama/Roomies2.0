CREATE VIEW rm2.vUser 
AS
SELECT UserId = u.UserId,
       Email = u.Email,
       HashedPassword = CASE WHEN p.HashedPassword IS NULL THEN 0x ELSE p.HashedPassword END,
       GoogleId = CASE WHEN g.GoogleId IS NULL THEN '' ELSE g.GoogleId END,
       GoogleRefreshToken = CASE WHEN g.RefreshToken IS NULL THEN '' ELSE g.RefreshToken END,
	   FacebookId = CASE WHEN f.FacebookId IS NULL THEN '' ELSE f.FacebookId END,
       FacebookRefreshToken = CASE WHEN f.RefreshToken IS NULL THEN '' ELSE  f.RefreshToken END
    FROM rm2.tUser u
             LEFT OUTER JOIN rm2.tPasswordUser p ON p.UserId = u.UserId
             LEFT OUTER JOIN rm2.tGoogleUser   g ON g.UserId = u.UserId
             LEFT OUTER JOIN rm2.tFacebookUser f ON f.UserId = u.UserId
    WHERE u.UserId <> 0;