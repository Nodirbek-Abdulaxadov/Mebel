using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<User>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Furniture> Furnitures { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<OtpModel> OtpModels { get; set; }

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

        builder.Entity<Furniture>()
            .HasMany(f => f.Colors)
            .WithMany(c => c.Furnitures)
            .UsingEntity(e => e.ToTable("FurnitureColors"));

        base.OnModelCreating(builder);
    }
}