using BusinessLogicLayer.Interfaces;
using DTOs.Extended;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) 
    : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [Authorize]
    [HttpGet("all/{lang}")]
    public async Task<IActionResult> GetAllAsync(Language lang)
    {
        var categories = await _categoryService.GetAllAsync(lang);
        await Task.Delay(1000);
        return Ok(categories);
    }

    //[HttpGet("paged")]
    //public async Task<IActionResult> GetAllAsync([FromQuery] int pageSize, [FromQuery] int pageNumber)
    //{
    //    var categories = await _categoryService.GetAllAsync(pageSize, pageNumber);
    //    var metadata = new
    //    {
    //        categories.TotalCount,
    //        categories.PageSize,
    //        categories.PageIndex,
    //        categories.TotalPages,
    //        categories.HasNextPage,
    //        categories.HasPreviousPage
    //    };

    //    Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);

    //    return Ok(categories.Items);
    //}

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetByIdAsync(int id)
    //{
    //    try
    //    {
    //        var category = await _categoryService.GetByIdAsync(id);
    //        return Ok(category);
    //    }
    //    catch (MarketException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    //[HttpPost]
    //public async Task<IActionResult> CreateAsync([FromBody] AddCategoryDto categoryDto)
    //{
    //    try
    //    {
    //        await _categoryService.CreateAsync(categoryDto);
    //        return Ok();
    //    }
    //    catch (MarketException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}

    //[HttpPut]
    //public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryDto categoryDto)
    //{
    //    try
    //    {
    //        var category = await _categoryService.UpdateAsync(categoryDto);
    //        return Ok(category);
    //    }
    //    catch (MarketException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    //    }
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteAsync(int id)
    //{
    //    try
    //    {
    //        await _categoryService.DeleteAsync(id);
    //        return Ok();
    //    }
    //    catch (MarketException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}
}
