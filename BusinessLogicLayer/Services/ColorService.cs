using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.ColorDtos;

namespace BusinessLogicLayer.Services;
public class ColorService
    (IUnitOfWork unitOfWork): IColorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Create new color
    /// </summary>
    /// <param name="colorDto"></param>
    /// <returns></returns>
    public async Task<ColorDto> CreatAsync(AddColorDto colorDto)
    {
        if (colorDto is null)
        {
            throw new MarketException(nameof(colorDto));
        }

        var color = (Color)colorDto;
        if (!color.IsValidColor())
        {
            throw new MarketException("Color is not valid");
        }

        var colors = await _unitOfWork.Colors.GetAllAsync();
        if (color.IsExist(colors))
        {
            throw new MarketException("Color is already exist");
        }

        _unitOfWork.Colors.Add(color);
        await _unitOfWork.SaveAsync();
        return (ColorDto)color;
    }

    /// <summary>
    /// Delete color by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task DeleteAsync(int id)
    {
        var color = await _unitOfWork.Colors.GetByIdAsync(id);
        if (color is null)
        {
            throw new MarketException("Color is not found");
        }

        _unitOfWork.Colors.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get all colors
    /// </summary>
    /// <returns></returns>
    public async Task<List<ColorDto>> GetAllAsync()
    {
        var colors = await _unitOfWork.Colors.GetAllAsync();
        return colors.Select(c => (ColorDto)c).ToList();
    }

    /// <summary>
    /// Get all colors with pagination
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<PagedList<ColorDto>> GetAllAsync(int pageSize, int pageNumber)
    {
        var colors = await _unitOfWork.Colors.GetAllAsync();
        var dtos = colors.Select(c => (ColorDto)c).ToList();
        return new PagedList<ColorDto>(dtos, dtos.Count, pageNumber, pageSize);
    }

    /// <summary>
    /// Get color by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<ColorDto> GetByIdAsync(int id)
    {
        var color = await _unitOfWork.Colors.GetByIdAsync(id);
        if (color is null)
        {
            throw new MarketException("Color is not found");
        }

        return (ColorDto)color;
    }

    /// <summary>
    /// Update color
    /// </summary>
    /// <param name="colorDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<ColorDto> UpdateAsync(ColorDto colorDto)
    {
        var color = await _unitOfWork.Colors.GetByIdAsync(colorDto.Id);
        if (color is null)
        {
            throw new MarketException("Color is not found");
        }

        color = (Color)colorDto;
        if (!color.IsValidColor())
        {
            throw new MarketException("Color is not valid");
        }

        var colors = await _unitOfWork.Colors.GetAllAsync();
        if (color.IsNotUnique(colors))
        {
            throw new MarketException("Color is already exist");
        }

        _unitOfWork.Colors.Update(color);
        await _unitOfWork.SaveAsync();
        return (ColorDto)color;
    }
}