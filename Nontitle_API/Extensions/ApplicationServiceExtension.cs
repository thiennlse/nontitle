using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nontitle_BusinessObject.Context;
using Nontitle_BusinessObject.Enum;
using Nontitle_BusinessObject.Models;

namespace Nontitle_API.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddDbContext<ApplicationDbContext>(context =>
        {
            context.UseNpgsql(configurations.GetConnectionString("default"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }

    public async static Task AddMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(Roles.Staff))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Staff));
        }
    }
}

