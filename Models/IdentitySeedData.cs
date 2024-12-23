using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Models
{
    public static class IdentitySeedData   // Class used to initialize the identity system and add initial data
    {
        private const string adminUser = "Admin";  //defined username
        private const string adminPassword = "AdminPassword123$"; //defined password
        private const string regularUser = "User";
        private const string regularPassword = "UserPassword123$";
        private const string adminRole = "Admin";
        private const string userRole = "User";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager =
                app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            var roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }

            IdentityUser? admin = await userManager.FindByNameAsync(adminUser);
            if (admin == null)
            {

                await userManager.CreateAsync(admin, adminPassword);
                await userManager.AddToRoleAsync(admin, adminRole);
            }

            IdentityUser? regular = await userManager.FindByNameAsync(regularUser);
            if (regular == null)
            {

                await userManager.CreateAsync(regular, regularPassword);
                await userManager.AddToRoleAsync(regular, userRole);
            }
        }
    }
}