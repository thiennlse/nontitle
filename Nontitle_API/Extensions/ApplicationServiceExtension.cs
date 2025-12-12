using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nontitle_BusinessObject.Context;
using Nontitle_BusinessObject.Enum;
using Nontitle_BusinessObject.Models;
using Nontitle_Repository.Implement;
using Nontitle_Repository.Infrastructure;
using Nontitle_Repository.Interfaces;
using Nontitle_Repository.Repositories;
using Nontitle_Service.Interfaces;
using Nontitle_Service.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json;

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

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IOrderCheckItemRepository, OrderCheckItemRepository>();
        services.AddScoped<IOrderCheckRepository, OrderCheckRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStoreRoleRepository, StoreRoleRepository>();
        return services;
    }

    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStoreService, StoreService>();
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

    public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Nontitle", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            options.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time",
                Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42\"")
            });
        });
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var secretKey = jwtSettings["SecretKey"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var response = context.Response;
                        response.ContentType = "application/json";

                        string message = "Token is invalid";
                        string errorCode = ApiCodes.TOKEN_INVALID;
                        int statusCode = StatusCodes.Status401Unauthorized;

                        if (context.AuthenticateFailure is SecurityTokenExpiredException)
                        {
                            message = "Token is expired";
                            errorCode = ApiCodes.TOKEN_EXPIRED;
                            statusCode = StatusCodes.Status403Forbidden;
                        }
                        else if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                        {
                            message = "No token provided";
                            errorCode = ApiCodes.UNAUTHENTICATED;
                        }

                        response.StatusCode = statusCode;

                        var result = JsonSerializer.Serialize(new
                        {
                            errorCode,
                            message,
                        });

                        await response.WriteAsync(result);
                    }
                };
            });

        return services;
    }
}

