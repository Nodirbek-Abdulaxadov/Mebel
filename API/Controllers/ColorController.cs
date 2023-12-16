using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOs.ColorDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ColorController(IColorService colorService)
    : ControllerBase
{
    private readonly IColorService _colorService = colorService;

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var colors = await _colorService.GetAllAsync();
        return Ok(colors);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var colors = await _colorService.GetAllAsync(pageSize, pageNumber);
        var metadata = new
        {
            colors.TotalCount,
            colors.PageSize,
            colors.PageIndex,
            colors.TotalPages,
            colors.HasNextPage,
            colors.HasPreviousPage
        };
        Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);
        return Ok(colors.Items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var color = await _colorService.GetByIdAsync(id);
            return Ok(color);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddColorDto color)
    {
        try
        {
            await _colorService.CreatAsync(color);
            return Ok(color);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(ColorDto color)
    {
        try
        {
            await _colorService.UpdateAsync(color);
            return Ok(color);
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _colorService.DeleteAsync(id);
            return Ok();
        }
        catch (MarketException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}