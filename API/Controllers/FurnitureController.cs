using BusinessLogicLayer.Interfaces;
using DTOs.FurnitureDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FurnitureController(IFurnitureService furnitureService)
    : ControllerBase
{
    private readonly IFurnitureService _furnitureService = furnitureService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var furnitureDtos = await _furnitureService.GetAllAsync();
        var json = JsonConvert.SerializeObject(furnitureDtos, Formatting.Indented, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        return Ok(json);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddFurnitureDto addFurnitureDto)
    {
        var furnitureDto = await _furnitureService.AddAsync(addFurnitureDto);
        return Ok();
    }
}