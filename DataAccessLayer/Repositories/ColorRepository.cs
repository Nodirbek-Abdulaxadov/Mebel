using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class ColorRepository (AppDbContext dbContext)
    : Repository<Color>(dbContext), IColorInterface
{
}