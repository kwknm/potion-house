namespace PotionHouse.Services;

public class FilesService : IFilesService
{
    private readonly IWebHostEnvironment _environment;

    public const string StaticDirName = "static";
    public const string ImagesDirName = "img";
    public const long MaxImageSize = 4 * 1024 * 1024; // 4MB

    public FilesService(IWebHostEnvironment environment)
    {
        _environment = environment;
        PrepareDirectories();
    }

    public async Task<bool> UploadImageAsync(IFormFile file)
    {
        if (file.Length > MaxImageSize)
            return false;

        var newFileName = Guid.NewGuid().ToString();
        var uploadPath = Path.Combine(_environment.WebRootPath, StaticDirName, ImagesDirName, newFileName);
        using var stream = new FileStream(uploadPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return true;
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
