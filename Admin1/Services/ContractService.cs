using Admin1.Contracts;
using Admin1.Extended;
using System.Net;
using System.Net.Http.Json;

namespace Admin1.Services;

public class ContractService<T>(HttpClient httpClient)
    : IContractService<T> where T : class
{
    protected readonly HttpClient _httpClient = httpClient;
    protected readonly Router<Category> _router = new();

    public async Task<ApiResult<T>> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync(_router.Delete(id));
        var result = response.StatusCode switch
        {
            HttpStatusCode.OK => new ApiResult<T>(true, "Category deleted successfully", null, null),
            HttpStatusCode.BadRequest => new ApiResult<T>(false, "Category could not be deleted", null, null),
            HttpStatusCode.Unauthorized => new ApiResult<T>(false, "Unauthorized", null, null),
            _ => new ApiResult<T>(false, "Something went wrong", null, null)
        };

        return result;
    }

    public async Task<ApiResult<T>> GetAllAsync()
    {
        var response =  await _httpClient.GetAsync(_router.GetAll());
        
        var result = response.StatusCode switch
        {
            HttpStatusCode.OK => new ApiResult<T>(true, "Categories fetched successfully", null, await response.Content.ReadFromJsonAsync<List<T>>()),
            HttpStatusCode.BadRequest => new ApiResult<T>(false, "Categories could not be fetched", null, null),
            HttpStatusCode.Unauthorized => new ApiResult<T>(false, "Unauthorized", null, null),
            _ => new ApiResult<T>(false, "Something went wrong", null, null)
        };

        return result;
    }

    public async Task<ApiResult<T>> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync(_router.GetById(id));
        var result = response.StatusCode switch
        {
            HttpStatusCode.OK => new ApiResult<T>(true, "Category fetched successfully", await response.Content.ReadFromJsonAsync<T>(), null),
            HttpStatusCode.BadRequest => new ApiResult<T>(false, "Category could not be fetched", null, null),
            HttpStatusCode.Unauthorized => new ApiResult<T>(false, "Unauthorized", null, null),
            _ => new ApiResult<T>(false, "Something went wrong", null, null)
        };
        return result;
    }
}