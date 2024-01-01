using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;
public class CategoryRepository(AppDbContext dbContext)
    : Repository<Category>(dbContext),
      ICategoryInterface
{
    
}