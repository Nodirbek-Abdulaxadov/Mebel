using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.CategoryDtos;
using DTOs.Extended;

namespace BusinessLogicLayer.Services;
public class CategoryService(IUnitOfWork unitOfWork)
    : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto,
                                               Language language)
    {
        if (categoryDto is null)
        {
            throw new MarketException("Category was null");
        }

        var model = (Category)categoryDto;
        if (!model.IsValidCategory())
        {
            throw new MarketException("Category is not valid");
        }

        var categories = await _unitOfWork.Categories.GetAllAsync();
        if (model.IsExist(categories))
        {
            throw new MarketException("Category already exists");
        }

        model = _unitOfWork.Categories.Add(model);
        await _unitOfWork.SaveAsync();
        return model.ToDto(language);
    }

    /// <summary>
    /// Action with category (archive, unarchive, delete)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task ActionAsync(int id, ActionType action)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null)
        {
            throw new MarketException("Category not found");
        }

        switch (action)
        {
            case ActionType.Archive:
                {
                    category.IsActive = false;
                    _unitOfWork.Categories.Update(category);
                } break;
            case ActionType.UnArchive:
                {
                    category.IsActive = true;
                    _unitOfWork.Categories.Update(category);
                } break;
            case ActionType.Delete:
                {
                    _unitOfWork.Categories.Delete(id);
                } break;
        }
        category.UpdatedAt = LocalTime.GetUtc5Time();
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get all categories with pagination
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber, Language language)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
        return new PagedList<CategoryDto>(categoryDtos, categoryDtos.Count, pageNumber, pageSize);
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    public async Task<List<CategoryDto>> GetAllAsync(Language language)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
        return categoryDtos;
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<CategoryDto> GetByIdAsync(int id, Language language)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        return category is null ? 
            throw new MarketException("Category not found") : category.ToDto(language);
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto, 
                                               Language language)
    {
        if (categoryDto is null)
        {
            throw new MarketException("Category was null");
        }

        var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
        if (category is null)
        {
            throw new MarketException("Category not found");
        }

        var model = (Category)categoryDto;
        if (!model.IsValidCategory())
        {
            throw new MarketException("Category is not valid");
        }

        var categories = await _unitOfWork.Categories.GetAllAsync();
        if (model.IsNotUnique(categories))
        {
            throw new MarketException("Category already exists");
        }

        _unitOfWork.Categories.Update(model);
        await _unitOfWork.SaveAsync();
        model = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
        if (model is null)
        {
            throw new MarketException("Category not found");
        }
        return model.ToDto(language);
    }

    public async Task<List<CategoryDto>> GetArchivedAsync(Language language)
    {
        var categories = await _unitOfWork.Categories.GetArchivedsAsync();
        var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
        return categoryDtos;
    }

    public async Task<PagedList<CategoryDto>> GetArchivedsAsPagedListAsync(int pageSize, int pageNumber, Language language)
    {
        var categories = await _unitOfWork.Categories.GetArchivedsAsync();
        var categoryDtos = categories.Select(c => c.ToDto(language)).ToList();
        return new PagedList<CategoryDto>(categoryDtos, categoryDtos.Count, pageNumber, pageSize);
    }
}