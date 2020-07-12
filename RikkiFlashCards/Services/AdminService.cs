using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RikkiFlashCards.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RikkiFlashCards.Services
{
    public class AdminService
    {
        public async static Task SetupAdminUserAsync(IServiceProvider serviceProvider, IConfiguration config)
        {
            var userManager = serviceProvider.GetService<UserManager<FlashCardUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var adminUser = await userManager.FindByNameAsync("Admin");
            if(adminUser == null)
            {
                adminUser = new FlashCardUser() 
                {
                    UserName = config.GetValue<string>("DefaultAdmin:UserName"),
                    Email = config.GetValue<string>("DefaultAdmin:Email")
                };
                var hashedPwd =config.GetValue<string>("DefaultAdmin:Password");
                var idResult = await userManager.CreateAsync(adminUser, hashedPwd);
                if(idResult.Succeeded)
                {
                    await CreateAndAddAdminRole(userManager, roleManager, adminUser);
                }
            }
            else
            {
                await CreateAndAddAdminRole(userManager, roleManager, adminUser);
            }
        }

        private static async Task CreateAndAddAdminRole(UserManager<FlashCardUser> userManager, RoleManager<IdentityRole> roleManager, FlashCardUser adminUser)
        {
            //create admin role if does not exist
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole is null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                if (roleResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else
            {
                var userHasAdminRole = await userManager.IsInRoleAsync(adminUser, "Admin");
                if (userHasAdminRole == false)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
