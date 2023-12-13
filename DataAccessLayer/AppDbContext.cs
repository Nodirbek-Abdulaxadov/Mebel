using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>()
            .HasMany(c => c.Furnitures)
            .WithOne(f => f.Category)
            .HasForeignKey(f => f.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Furniture>()
            .HasMany(f => f.Images)
            .WithOne(i => i.Furniture)
            .HasForeignKey(i => i.FurnitureId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FurnitureColor>()
            .HasKey(fc => new { fc.FurnitureId, fc.ColorId });

        builder.Entity<FurnitureColor>()
            .HasOne(fc => fc.Furniture)
            .WithMany(f => f.FurnitureColors)
            .HasForeignKey(fc => fc.FurnitureId);

        builder.Entity<FurnitureColor>()
            .HasOne(fc => fc.Color)
            .WithMany(c => c.FurnitureColors)
            .HasForeignKey(fc => fc.ColorId);

        base.OnModelCreating(builder);
    }
}