using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser 
                {
                    DisplayName = "Juan",
                    Email = "juan@test.com",
                    UserName = "juan@test.com", 
                };

                await userManager.CreateAsync(user, "Pa$$w0rd"); 
            }; 
        }
    }
}