using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Services;
public class ImageService : IImageService
{
    /// <summary>
    /// Delete image from server by url 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="folder"></param>
    /// <returns></returns>
    public Task DeleteAsync(string url, string folder)
    {
        string file = string.Empty;
        if (url.Contains('/'))
        {
            file = url.Split('/')[^1];
        }
        else
        {
            file = url.Split("%2F")[^1];
        }

        var filePath = Path.Combine(folder, "uploads", file);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Upload image to server and return url
    /// </summary>
    /// <param name="file"></param>
    /// <param name="folder"></param>
    /// <param name="domain"></param>
    /// <returns></returns>
    public async Task<string> UploadAsync(IFormFile file, string folder, string domain)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(folder, "uploads", fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        string url = $"{domain}/uploads/{fileName}";

        return url;
    }
}