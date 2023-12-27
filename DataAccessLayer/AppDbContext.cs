using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }

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
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Entity<Furniture>()
            .HasMany(f => f.Images)
            .WithOne(i => i.Furniture)
            .HasForeignKey(i => i.FurnitureId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Entity<Furniture>()
            .HasMany(f => f.Colors)
            .WithMany(c => c.Furnitures)
            .UsingEntity(e => e.ToTable("FurnitureColors"));

        builder.Entity<User>()
            .HasMany(u => u.LikedItems)
            .WithMany(f => f.LikedUsers)
            .UsingEntity(e => e.ToTable("UserLikedItems"));

        builder.Entity<User>()
            .HasMany(u => u.Feedbacks)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        base.OnModelCreating(builder);
    }
}