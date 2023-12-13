using BusinessLogicLayer.Extended;
using DTOs.CategoryDtos;

namespace BusinessLogicLayer.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber);
    Task<CategoryDto> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto);
    Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto);
    Task DeleteAsync(int id);
}