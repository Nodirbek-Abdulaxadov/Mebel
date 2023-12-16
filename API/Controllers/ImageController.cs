using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController(IImageService imageService,
                             IWebHostEnvironment environment,
                             IConfiguration configuration)
    : ControllerBase
{
    private readonly IImageService _imageService = imageService;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        string folder = _environment.WebRootPath;
        string domain = _configuration["Domain"]??"";

        var result = await _imageService.UploadAsync(file, folder, domain);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage(string url)
    {
        string folder = _environment.WebRootPath;
        await _imageService.DeleteAsync(url, folder);
        return Ok();
    }
}