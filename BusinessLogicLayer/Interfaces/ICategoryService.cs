using BusinessLogicLayer.Extended;
using DTOs.CategoryDtos;
using DTOs.Extended;

namespace BusinessLogicLayer.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync(Language language);
    Task<List<CategoryDto>> GetArchivedAsync(Language language);
    Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber, Language language);
    Task<PagedList<CategoryDto>> GetArchivedsAsPagedListAsync(int pageSize, int pageNumber, Language language);
    Task<CategoryDto> GetByIdAsync(int id, Language language);
    Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto, Language language);
    Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto, Language language);
    Task ActionAsync(int id, ActionType action);
}