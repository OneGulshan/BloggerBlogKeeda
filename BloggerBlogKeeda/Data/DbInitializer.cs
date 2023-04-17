using BloggerBlogKeeda.Models;
using Microsoft.AspNetCore.Identity;

namespace BloggerBlogKeeda.Data
{
    public static class DbInitializer
    {
        public static async Task InitializerAsync(IServiceProvider serviceProvider, UserManager<AppUser> _userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult result;
            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            string Email = "admin@gmail.com";
            string password = "Admin,./123";

            if (_userManager.FindByEmailAsync(Email).Result == null)
            {
                AppUser user = new()
                {
                    UserName = Email,
                    Email = Email
                };
                IdentityResult resultIdentity = _userManager.CreateAsync(user, password).Result;
                if (resultIdentity.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}