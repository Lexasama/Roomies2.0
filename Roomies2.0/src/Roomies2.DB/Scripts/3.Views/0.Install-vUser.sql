CREATE VIEW rm2.vUser AS
SELECT tU.UserId,
       tU.Email,
       tU.UserName,
       tPU.HashedPassword,
       tGU.GoogleId,
       tGU.RefreshToken AS GRefreshToken,
       tFU.FacebookId,
       tFU.RefreshToken AS FRefreshToken
    FROM tUser                       tU
             LEFT JOIN tPasswordUser tPU ON tU.UserId = tPU.UserId
             LEFT JOIN tGoogleUser   tGU ON tU.UserId = tGU.UserId
             LEFT JOIN tFacebookUser tFU ON tU.UserId = tFU.UserId

    WHERE tU.UserId <> 0;