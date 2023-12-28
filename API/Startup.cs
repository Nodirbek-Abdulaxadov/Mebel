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
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Furniture API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
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
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role
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
        app.SeedRolesToDatabase().Wait();
    }

    private static async Task SeedRolesToDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "User", "SuperAdmin" };
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
            PhoneNumber = "+998996555744",
            Address = "Database",
            AvatarUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTNq-fhMeQRIAFfcfgPFaQDO8yTQ_SOW1-6raA_0HgiiKDJTV0TkDiojPT98h40g8T4FAk&usqp=CAU",
            BirthDate = DateOnly.Parse(DateTime.Now.ToShortDateString()),
            Gender = 0
        };
        var adminPassword = "Admin.123$";
        var user = await userManager.FindByNameAsync(admin.UserName);
        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(admin, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "SuperAdmin");
            }
        }
    }
}