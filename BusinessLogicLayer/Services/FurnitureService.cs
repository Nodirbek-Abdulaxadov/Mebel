using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.FurnitureDtos;
using DTOs.Extended;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLogicLayer.Services;

public class FurnitureService(IUnitOfWork unitOfWork,
                              IImageService imageService,
                             IWebHostEnvironment environment)
    : IFurnitureService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageService _imageService = imageService;
    private IWebHostEnvironment _environment = environment;

    /// <summary>
    /// Create new furniture
    /// </summary>
    /// <param name="furnitureDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<FurnitureDto> CreateAsync(AddFurnitureDto furnitureDto,
                                               Language language)
    {
        if (furnitureDto is null)
        {
            throw new MarketException("Furniture was null");
        }

        var model = (Furniture)furnitureDto;
        if (!model.IsValidFurniture())
        {
            throw new MarketException("Furniture is not valid");
        }

        var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
        if (model.IsExist(furnitures))
        {
            throw new MarketException("Furniture already exists");
        }

        var colorIds = furnitureDto.ColorIds
                                   .ToHashSet()
                                   .ToList();
        var colors = await _unitOfWork.Colors.GetAllAsync();
        foreach (var colorId in colorIds)
        {
            var color = colors.FirstOrDefault(c => c.Id == colorId);
            if (color is null)
            {
                throw new MarketException("Color not found");
            }
            model.Colors!.Add(color);
        }

        model = _unitOfWork.Furnitures.Add(model);
        await _unitOfWork.SaveAsync();

        foreach (var imageUrl in furnitureDto.ImageUrls)
        {
            var image = new Image
            {
                FurnitureId = model.Id,
                Url = imageUrl
            };
            _unitOfWork.Images.Add(image);
            await _unitOfWork.SaveAsync();
        }

        return model.ToDto(language);
    }

    /// <summary>
    /// Action with furniture (archive, unarchive, delete)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task ActionAsync(int id, ActionType action)
    {
        var furniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
        if (furniture is null)
        {
            throw new MarketException("Furniture not found");
        }

        switch (action)
        {
            case ActionType.Archive:
                {
                    furniture.IsActive = false;
                    _unitOfWork.Furnitures.Update(furniture);
                }
                break;
            case ActionType.UnArchive:
                {
                    furniture.IsActive = true;
                    _unitOfWork.Furnitures.Update(furniture);
                }
                break;
            case ActionType.Delete:
                {
                    var images = furniture.Images.ToList();
                    foreach (var image in images)
                    {
                        string folder = _environment.WebRootPath;
                        await _imageService.DeleteAsync(image.Url, folder);
                        _unitOfWork.Images.Delete(image.Id);
                    }
                    _unitOfWork.Furnitures.Delete(id);
                    await _unitOfWork.SaveAsync();
                }
                break;
        }
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get all furnitures with pagination
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<PagedList<FurnitureDto>> GetAllAsync(int pageSize, int pageNumber, Language language)
    {
        var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
        var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
        return new PagedList<FurnitureDto>(furnitureDtos, furnitureDtos.Count, pageNumber, pageSize);
    }

    /// <summary>
    /// Get all furnitures
    /// </summary>
    /// <returns></returns>
    public async Task<List<FurnitureDto>> GetAllAsync(Language language)
    {
        var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
        var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
        return furnitureDtos;
    }

    /// <summary>
    /// Get furniture by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<FurnitureDto> GetByIdAsync(int id, Language language)
    {
        var furniture = await _unitOfWork.Furnitures.GetByIdAsync(id);
        return furniture is null ?
            throw new MarketException("Furniture not found") : furniture.ToDto(language);
    }

    /// <summary>
    /// Update furniture
    /// </summary>
    /// <param name="furnitureDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<FurnitureDto> UpdateAsync(UpdateFurnitureDto furnitureDto,
                                               Language language)
    {
        if (furnitureDto is null)
        {
            throw new MarketException("Furniture was null");
        }

        var furniture = await _unitOfWork.Furnitures.GetByIdAsync(furnitureDto.Id);
        if (furniture is null)
        {
            throw new MarketException("Furniture not found");
        }

        var model = (Furniture)furnitureDto;
        if (!model.IsValidFurniture())
        {
            throw new MarketException("Furniture is not valid");
        }

        var furnitures = await _unitOfWork.Furnitures.GetAllAsync();
        if (model.IsNotUnique(furnitures))
        {
            throw new MarketException("Furniture already exists");
        }

        _unitOfWork.Furnitures.Update(model);
        await _unitOfWork.SaveAsync();
        model = await _unitOfWork.Furnitures.GetByIdAsync(furnitureDto.Id);
        if (model is null)
        {
            throw new MarketException("Furniture not found");
        }
        return model.ToDto(language);
    }

    /// <summary>
    /// Get archived all furnitures
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public async Task<List<FurnitureDto>> GetArchivedAsync(Language language)
    {
        var furnitures = await _unitOfWork.Furnitures.GetArchivedsAsync();
        var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
        return furnitureDtos;
    }

    /// <summary>
    /// Get archived furnitures with pagination
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public async Task<PagedList<FurnitureDto>> GetArchivedsAsPagedListAsync(int pageSize, int pageNumber, Language language)
    {
        var furnitures = await _unitOfWork.Furnitures.GetArchivedsAsync();
        var furnitureDtos = furnitures.Select(c => c.ToDto(language)).ToList();
        return new PagedList<FurnitureDto>(furnitureDtos, furnitureDtos.Count, pageNumber, pageSize);
    }
}