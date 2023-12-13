﻿using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DTOs.CategoryDtos;

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
    public async Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto)
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
        return (CategoryDto)model;
    }

    /// <summary>
    /// Delete category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task DeleteAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null)
        {
            throw new MarketException("Category not found");
        }

        _unitOfWork.Categories.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get all categories with pagination
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    public async Task<PagedList<CategoryDto>> GetAllAsync(int pageSize, int pageNumber)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = categories.Select(c => (CategoryDto)c).ToList();
        return new PagedList<CategoryDto>(categoryDtos, categoryDtos.Count, pageNumber, pageSize);
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = categories.Select(c => (CategoryDto)c).ToList();
        return categoryDtos;
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        return category is null ? 
            throw new MarketException("Category not found") : (CategoryDto)category;
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    /// <exception cref="MarketException"></exception>
    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto categoryDto)
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
        return (CategoryDto)categoryDto;
    }
}