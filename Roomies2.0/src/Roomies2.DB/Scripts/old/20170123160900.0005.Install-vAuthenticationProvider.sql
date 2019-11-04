create view rm2.vAuthenticationProvider
as
    select usr.UserId, usr.ProviderName
    from (select UserId = u.UserId,
              ProviderName = 'Roomies2.0'
          from rm2.tPasswordUser u
          union all
          select UserId = u.UserId,
              ProviderName = 'Google'
          from rm2.tGoogleUser u) usr
    where usr.UserId <> 0;