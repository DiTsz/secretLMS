using reSmart.Models;

namespace reSmart.Data
{
    public static class SeedData
    {
        public static async Task Initialize(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            var roles = new[] { "Admin", "Teacher", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create admin user
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser { UserName = adminEmail, Email = adminEmail, FirstName = "Admin User" };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
