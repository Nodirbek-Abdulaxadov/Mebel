using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;
public interface IFurnitureInterface
    : IRepository<Furniture>
{
    Task<IEnumerable<Furniture>> GetAllAsyncWithDependencies();
    Task<Furniture?> GetByIdAsyncWithDependencies(int id);
}