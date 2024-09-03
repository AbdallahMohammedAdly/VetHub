using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;

namespace VetHub02.Repository.Identity
{
    public static class AppIdentityDbContextDataSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager )
        {
            var adminUser = await userManager.FindByEmailAsync("abdallahedly@gmail.com");

            if (adminUser == null)
            {
                adminUser = new AppUser()
                {
                    DisplayName = "Abdullah Mohammed",
                    Email = "abdallahedly@gmail.com",
                    UserName = "abdallahedly",
                    PhoneNumber = "01145676722"
                };

                var role = new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                };

                var roleExists = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(role);
                }

                var result = await userManager.CreateAsync(adminUser, "Abdullah@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, role.Name);
                }
            }

        }
    }
}
