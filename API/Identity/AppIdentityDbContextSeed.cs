using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager
        )
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Juan",
                    Email = "juan@test.com",
                    UserName = "juan@test.com",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
            ;
            if (!roleManager.Roles.Any())
            {
                var roles = new List<AppRole>
                {
                    new AppRole { Name = "Vet", },
                    new AppRole { Name = "Owner", },
                    new AppRole { Name = "Admin", },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
