using Admin1.Extended;

namespace Admin1.Contracts;

public interface IContractService<T> where T : class
{
    Task<ApiResult<T>> GetAllAsync();
    Task<ApiResult<T>> GetByIdAsync(int id);
    Task<ApiResult<T>> DeleteAsync(int id);
}