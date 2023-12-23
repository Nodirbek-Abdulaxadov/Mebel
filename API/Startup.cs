using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API;

public static class Startup
{
    private const string _defaultCorsPolicyName = "localhost";

    public static void AddDIServices(this WebApplicationBuilder builder)
    {
        #region Default DI Services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        #endregion

        #region DBContext
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
        });
        #endregion

        #region Identity
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

        //add role manager to DI

        #endregion

        #region Custom DI Services
        builder.Services.AddTransient<ICategoryInterface, CategoryRepository>();
        builder.Services.AddTransient<IColorInterface, ColorRepository>();
        builder.Services.AddTransient<IFurnitureInterface, FurnitureRepository>();
        builder.Services.AddTransient<IImageInterface, ImageRepository>();
        builder.Services.AddTransient<IOtpModelInterface, OtpModelRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IColorService, ColorService>();
        builder.Services.AddTransient<IImageService, ImageService>();
        builder.Services.AddTransient<IFurnitureService, FurnitureService>();
        builder.Services.AddTransient<IUserService, UserService>();
        #endregion

        #region CORS Policy for all origins
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(_defaultCorsPolicyName, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
        #endregion

        #region JWT
        var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>()??"key";
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtIssuer,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuerSigningKey = true
                };
            });

        #endregion


        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public static void AddMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseCors(_defaultCorsPolicyName);
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.SeedRolesToDatabase();
    }

    private static async void SeedRolesToDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
            {
                var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
            }
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var admin = new User
        {
            UserName = "+998996555744",
            PhoneNumberConfirmed = true,
            PhoneNumber = "+998996555744"
        };
        var adminPassword = "Admin.123$";
        var user = await userManager.FindByNameAsync(admin.UserName);
        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(admin, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}