using System.IO;
using System.Threading.Tasks;

namespace RememBeer.Common.Services.Contracts
{
    public interface IImageUploadService
    {
        Task<string> UploadImageAsync(Stream image, int width, int height);
    }
}
