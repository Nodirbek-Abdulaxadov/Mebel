using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class FurnitureRepository(AppDbContext dbContext)
    : Repository<Furniture>(dbContext), IFurnitureInterface
{
}