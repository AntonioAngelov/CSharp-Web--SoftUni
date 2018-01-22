namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using CameraBazaar.Data.Models;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                                            .GetRequiredService<IServiceScopeFactory>()
                                            .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<CameraBazaarDbContext>()
                    .Database
                    .Migrate();


                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = GlobalConstants.AdminRole;
                        var userRole = GlobalConstants.UserRole;
                        var restrictedUserRole = GlobalConstants.UserRestrict;

                        var isAdminRoleExist = await roleManager.RoleExistsAsync(adminName);
                        var isUserRoleExist = await roleManager.RoleExistsAsync(userRole);
                        var isUserRestrictRoleExist = await roleManager.RoleExistsAsync(restrictedUserRole);

                        if (!isUserRestrictRoleExist)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = restrictedUserRole
                            });
                        }

                        if (!isUserRoleExist)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = userRole
                            });
                        }

                        if (!isAdminRoleExist)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = adminName
                            });
                        }

                        var adminUser = await userManager.FindByNameAsync(adminName);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                UserName = adminName,
                                Email = "admin@admin.admin"
                            };

                            await userManager.CreateAsync(adminUser, "admin123");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                .Wait();
            }

            return app;
        }
    }
}
