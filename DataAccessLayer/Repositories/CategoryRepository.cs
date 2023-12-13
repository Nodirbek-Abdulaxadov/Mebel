using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class CategoryRepository(AppDbContext dbContext)
    : Repository<Category>(dbContext), 
      ICategoryInterface
{
}