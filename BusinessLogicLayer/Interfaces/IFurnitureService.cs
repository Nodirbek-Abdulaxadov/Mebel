using BusinessLogicLayer.Extended;
using DTOs.FurnitureDtos;

namespace BusinessLogicLayer.Interfaces;
public interface IFurnitureService
{
    Task<List<FurnitureDto>> GetAllAsync();
    Task<PagedList<FurnitureDto>> GetPagedListAsync(int pageSize, int pageNumber);
    Task<FurnitureDto> GetByIdAsync(int id);
    Task<FurnitureDto> AddAsync(AddFurnitureDto furnitureDto);
    Task<FurnitureDto> UpdateAsync(FurnitureDto furnitureDto);
    Task DeleteAsync(int id);
}