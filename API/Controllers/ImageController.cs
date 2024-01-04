using Asp.Versioning;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "User, Admin, SuperAdmin")]
public class ImageController(IImageService imageService,
                             IWebHostEnvironment environment,
                             IConfiguration configuration)
    : ControllerBase
{
    private readonly IImageService _imageService = imageService;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            string folder = _environment.WebRootPath;
            string domain = _configuration["Domain"] ?? "";

            var result = await _imageService.UploadAsync(file, folder, domain);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteImage(string url)
    {
        try
        {
            string folder = _environment.WebRootPath;
            await _imageService.DeleteAsync(url, folder);
            return Ok();
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}