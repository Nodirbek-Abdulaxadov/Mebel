using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.FurnitureDtos;

namespace BusinessLogicLayer.Services;
public class FurnitureService(IUnitOfWork unitOfWork,
                              IImageService imageService)
    : IFurnitureService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageService _imageService = imageService;

    public async Task<FurnitureDto> AddAsync(AddFurnitureDto furnitureDto)
    {
        var furniture = (Furniture)furnitureDto;
        var colors = await _unitOfWork.Colors.GetAllAsync();
        furniture.Colors = furnitureDto.ColorIds
                                   .Select(id => 
                                   colors.First(i => i.Id == id))
                                   .ToList();
        furniture = _unitOfWork.Furnitures.Add(furniture);
        await _unitOfWork.SaveAsync();

        foreach (var image in furnitureDto.ImageUrls)
        {
            var imageDto = new Image
            {
                FurnitureId = furniture.Id,
                Url = image
            };
            _unitOfWork.Images.Add(imageDto);
            await _unitOfWork.SaveAsync();
        }

        return (FurnitureDto)furniture;
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FurnitureDto>> GetAllAsync()
    {
        var list = await _unitOfWork.Furnitures.GetAllAsyncWithDependencies();
        var dtos = list.Select(f => (FurnitureDto)f).ToList();
        return dtos;
    }

    public Task<FurnitureDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<FurnitureDto>> GetPagedListAsync(int pageSize, int pageNumber)
    {
        var list = await _unitOfWork.Furnitures.GetAllAsync();
        var dtos = list.Select(f => (FurnitureDto)f).ToList();
        var pagedList = new PagedList<FurnitureDto>(dtos, dtos.Count, pageNumber, pageSize);
        return pagedList;
    }

    public Task<FurnitureDto> UpdateAsync(FurnitureDto furnitureDto)
    {
        throw new NotImplementedException();
    }
}
