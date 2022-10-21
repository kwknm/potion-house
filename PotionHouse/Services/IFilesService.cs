namespace PotionHouse.Services
{
    public interface IFilesService
    {
        Task<bool> UploadImageAsync(IFormFile file);
        Task UploadMultipleImagesAsync(List<IFormFile> files);
    }
}