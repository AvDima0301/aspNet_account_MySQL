using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using News.Web.Constants;
using News.Web.Data.Entities.Identity;

namespace News.Web.Data
{
    public static class SeederDB
    {
        public static void SeedData(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    //throw new Exception("Problem seeder database!");
                    logger.LogInformation("---Seeding Web And Localization Databases---");
                    var context = services.GetRequiredService<AppEFContext>();
                    context.Database.Migrate();
                    InitRolesAndUsers(services);
                }
                catch(Exception ex)
                {
                    logger.LogError("-----Problem seed database-----");
                }

            }
        }

        private static void InitRolesAndUsers(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            if(!roleManager.Roles.Any())
            {
                var adminRole = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;

                var userRole = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
            }
            if (!userManager.Users.Any())
            {
                string email = "admin@gmail.com";
                var user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = "Петро",
                    SecondName = "Шпрот",
                    Phone = "+38(098)232 34 22",
                    Photo = "brekunti.5hy.jpeg"
                };
                var result = userManager.CreateAsync(user, "12345").Result;
                if (result.Succeeded)
                {
                   
                    result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }
        }
    }
}
