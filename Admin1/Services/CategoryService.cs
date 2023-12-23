using Admin1.Contracts;
using Admin1.Extended;
using DTOs.CategoryDtos;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Admin1.Services;

public class CategoryService(HttpClient httpClient) 
    : ContractService<CategoryDto>(httpClient), ICategoryService
{
    public async Task<ApiResult<CategoryDto>> CreateAsync(AddCategoryDto dto)
    {
        var json = JsonConvert.SerializeObject(dto);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_router.BASE, data);

        var result = response.StatusCode switch
        {
            HttpStatusCode.OK => new ApiResult<CategoryDto>(true, "Category created successfully", null, null),
            HttpStatusCode.BadRequest => new ApiResult<CategoryDto>(false, "Category could not be created", null, null),
            HttpStatusCode.Unauthorized => new ApiResult<CategoryDto>(false, "Unauthorized", null, null),
            _ => new ApiResult<CategoryDto>(false, "Something went wrong", null, null)
        };

        return result;
    }

    public async Task<ApiResult<CategoryDto>> UpdateAsync(UpdateCategoryDto dto)
    {
        var json = JsonConvert.SerializeObject(dto);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_router.BASE, data);

        var result = response.StatusCode switch
        {
            HttpStatusCode.OK => new ApiResult<CategoryDto>(true, "Category updated successfully", null, null),
            HttpStatusCode.BadRequest => new ApiResult<CategoryDto>(false, "Category could not be created", null, null),
            HttpStatusCode.Unauthorized => new ApiResult<CategoryDto>(false, "Unauthorized", null, null),
            _ => new ApiResult<CategoryDto>(false, "Something went wrong", null, null)
        };

        return result;
    }
}