using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class ImageRepository(AppDbContext dbContext)
    : Repository<Image>(dbContext), IImageInterface
{
}