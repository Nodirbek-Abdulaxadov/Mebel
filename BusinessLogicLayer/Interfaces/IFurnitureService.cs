using BusinessLogicLayer.Extended;
using DTOs.FurnitureDtos;
using DTOs.Extended;

namespace BusinessLogicLayer.Interfaces;
public interface IFurnitureService
{
    Task<List<FurnitureDto>> GetAllAsync(Language language);
    Task<PagedList<FurnitureDto>> GetAllAsync(int pageSize, int pageNumber, Language language);
    Task<FurnitureDto> GetByIdAsync(int id, Language language);
    Task<FurnitureDto> CreateAsync(AddFurnitureDto furnitureDto, Language language);
    Task<FurnitureDto> UpdateAsync(UpdateFurnitureDto furnitureDto, Language language);
    Task ActionAsync(int id, ActionType action);
}