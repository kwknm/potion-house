using PotionHouse.Services.Abstractions;

namespace PotionHouse.Services;

public class FilesService : IFilesService
{
    private readonly IWebHostEnvironment _environment;

    private const string StaticDirName = "static";
    private const string ImagesDirName = "img";
    private const long MaxImageSize = 4 * 1024 * 1024; // 4MB

    public FilesService(IWebHostEnvironment environment)
    {
        _environment = environment;
        PrepareDirectories();
    }

    public async Task<string?> UploadImageAsync(IFormFile file)
    {
        if (file.Length > MaxImageSize)
            return null;

        var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var uploadPath = Path.Combine(_environment.WebRootPath, StaticDirName, ImagesDirName, newFileName);
        
        await using var stream = new FileStream(uploadPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var imagePath = Path.Combine(StaticDirName, ImagesDirName, newFileName);
        return imagePath;
    }

    public async Task UploadMultipleImagesAsync(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            await UploadImageAsync(file);
        }
    }

    private void PrepareDirectories()
    {
        var webRootPath = _environment.WebRootPath;

        var staticDir = Path.Combine(webRootPath, StaticDirName);
        if (!Directory.Exists(staticDir))
            Directory.CreateDirectory(staticDir);

        var imagesDir = Path.Combine(staticDir, ImagesDirName);
        if (!Directory.Exists(imagesDir))
            Directory.CreateDirectory(imagesDir);

        // todo: add videos support in the future
    }
}
