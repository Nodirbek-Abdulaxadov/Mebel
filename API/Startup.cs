using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class Startup
{
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
        #endregion

        #region Custom DI Services
        builder.Services.AddTransient<ICategoryInterface, CategoryRepository>();
        builder.Services.AddTransient<IColorInterface, ColorRepository>();
        builder.Services.AddTransient<IFurnitureInterface, FurnitureRepository>();
        builder.Services.AddTransient<IImageInterface, ImageRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IColorService, ColorService>();
        #endregion


    }

    public static void AddMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}