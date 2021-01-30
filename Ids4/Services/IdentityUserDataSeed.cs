using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRepositories.Repositories;
using System;
using System.Linq;
using System.Security.Claims;

namespace Ids4.Services
{
    public class IdentityUserDataSeed<UserContext> : IDataSeed where UserContext : IdentityDbContext<IdentityUser>
    {
        public void InitData(IServiceProvider serviceProvider)
        {
            var userContext = serviceProvider.GetRequiredService<UserContext>();
            userContext.Database.Migrate();

            if (!userContext.Users.Any())
            {
                userContext.Users.Add(new IdentityUser
                {
                    Email = "aaa@aa.com",
                    NormalizedUserName = "AAA@AA.COM",
                    UserName = "aaa@aa.com",
                    NormalizedEmail = "AAA@AA.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEMrhvfbyKlt5C1ImFyIh9A3kwfi2Myes9gQARaG/FxCSTSQvrYo8xNT70uUef85ZaA==",
                    SecurityStamp = "WBMZWQUYRJSSEYUDY5V7K4XMAY5BTHAF",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true
                });
            }
            //if (!userContext.UserClaims.Any())
            //{
            //    userContext.UserClaims.Add(new IdentityUserClaim<>
            //    {
            //        UserId = "",
            //        ClaimType = ClaimTypes.Name,
            //        ClaimValue = "aaa@aa.com"
            //    });
            //}

            userContext.SaveChanges();
        }
    }
}
