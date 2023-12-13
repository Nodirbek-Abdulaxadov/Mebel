using BusinessLogicLayer.Extended;
using DTOs.ColorDtos;

namespace BusinessLogicLayer.Interfaces;
public interface IColorService
{
    Task<List<ColorDto>> GetAllAsync();
    Task<PagedList<ColorDto>> GetAllAsync(int pageSize, int pageNumber);
    Task<ColorDto> GetByIdAsync(int id);
    Task<ColorDto> CreatAsync(AddColorDto colorDto);
    Task<ColorDto> UpdateAsync(ColorDto colorDto);
    Task DeleteAsync(int id);
}