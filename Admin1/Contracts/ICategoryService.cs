using Admin1.Extended;
using DTOs.CategoryDtos;

namespace Admin1.Contracts;

public interface ICategoryService 
    : IContractService<CategoryDto>
{
    Task<ApiResult<CategoryDto>> CreateAsync(AddCategoryDto dto);
    Task<ApiResult<CategoryDto>> UpdateAsync(UpdateCategoryDto dto);
}