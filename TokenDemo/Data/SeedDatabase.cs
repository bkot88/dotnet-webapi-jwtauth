
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TokenDemo.Data
{
    public class SeedDatabase
    {
        public static void Init(IServiceProvider service)
        {
            var context = service.GetRequiredService<PostgresDbContext>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "firstname.surname@domain.com"
                };
                userManager.CreateAsync(user, "Password@123");
            }
        }
    }
}
