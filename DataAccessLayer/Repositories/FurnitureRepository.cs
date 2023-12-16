using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;
public class FurnitureRepository(AppDbContext dbContext)
    : Repository<Furniture>(dbContext), IFurnitureInterface
{
    public async Task<IEnumerable<Furniture>> GetAllAsyncWithDependencies()
        => await _dbContext.Furnitures
            .Include(f => f.Category)
            .Include(f => f.Images)
            .Include(f => f.Colors)
            .ToListAsync();
}