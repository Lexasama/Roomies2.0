CREATE VIEW rm2.vAuthenticationProvider
AS
SELECT usr.UserId, usr.ProviderName
    FROM (SELECT UserId       = u.UserId,
                 ProviderName = 'Roomies2.0'
              FROM rm2.tPasswordUser u
          UNION ALL
          SELECT UserId       = u.UserId,
                 ProviderName = 'Facebook'
              FROM rm2.tfacebookUser u
          UNION ALL
          SELECT UserId       = u.UserId,
                 ProviderName = 'Google'
              FROM rm2.tGoogleUser u) usr
    WHERE usr.UserId <> 0;