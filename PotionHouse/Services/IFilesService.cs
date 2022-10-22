namespace PotionHouse.Services
{
    public interface IFilesService
    {
        Task<string?> UploadImageAsync(IFormFile file);
        Task UploadMultipleImagesAsync(List<IFormFile> files);
    }
}