namespace EcommerceApp.Interfaces
{
    public interface IFileManager
    {
        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile image);
    }
}
