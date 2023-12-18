using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Contracts;

public interface IBaseRepository<T> where T : class
{
    Task<T> Get(int id);
    Task<IList<T>> Get();
    Task<bool> Create(T obj);
    Task<bool> Update(T obj, int id);
    Task<bool> Delete(int id);
}