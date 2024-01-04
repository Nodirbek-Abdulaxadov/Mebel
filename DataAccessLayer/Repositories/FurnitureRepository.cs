using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;
public class FurnitureRepository(AppDbContext dbContext)
    : Repository<Furniture>(dbContext), IFurnitureInterface
{
    public async Task<IEnumerable<Furniture>> GetAllAsyncWithDependencies()
        => await _dbContext.Furnitures
            .AsNoTracking()
            .Include(f => f.Category)
            .Include(f => f.Images)
            .Include(f => f.LikedUsers)
            .Include(f => f.Colors)
            .ThenInclude(c => c.Color)
            .ToListAsync();

    public async Task<Furniture?> GetByIdAsyncWithDependencies(int id)
        => await _dbContext.Furnitures
            .AsNoTracking()
            .Include(f => f.Category)
            .Include(f => f.Images)
            .Include(f => f.LikedUsers)
            .Include(f => f.Colors)
            .ThenInclude(c => c.Color)
            .FirstOrDefaultAsync(f => f.Id == id);
}