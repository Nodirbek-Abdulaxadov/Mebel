using BusinessLogicLayer.Extended;
using DTOs.ColorDtos;
using DTOs.Extended;

namespace BusinessLogicLayer.Interfaces;
public interface IColorService
{
    Task<List<ColorDto>> GetAllAsync(Language language);
    Task<List<ColorDto>> GetArchivedAsync(Language language);
    Task<PagedList<ColorDto>> GetAllAsync(int pageSize, int pageNumber, Language language);
    Task<PagedList<ColorDto>> GetArchivedsAsPagedListAsync(int pageSize, int pageNumber, Language language);
    Task<ColorDto> GetByIdAsync(int id, Language language);
    Task<SingleColorDto> GetById(int id);
    Task<ColorDto> CreateAsync(AddColorDto colorDto, Language language);
    Task<ColorDto> UpdateAsync(UpdateColorDto colorDto, Language language);
    Task ActionAsync(int id, ActionType action);
}