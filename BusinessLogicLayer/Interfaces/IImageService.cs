using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Interfaces;
public interface IImageService
{
    Task<string> UploadAsync(IFormFile file, 
                                  string folder, 
                                  string domain);
    Task DeleteAsync(string url, string folder);
}