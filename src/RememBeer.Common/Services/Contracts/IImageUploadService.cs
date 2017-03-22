using System.Threading.Tasks;

namespace RememBeer.Common.Services.Contracts
{
    public interface IImageUploadService
    {
        Task<string> UploadImageAsync(byte[] image, int width, int height);
    }
}
