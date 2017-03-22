using System;
using System.IO;
using System.Threading.Tasks;

using RememBeer.Common.Services.Contracts;

namespace RememBeer.Common.Services
{
    public class DebugImageService : IImageUploadService
    {
        public Task<string> UploadImageAsync(Stream image, int width, int height)
        {
            var url = "http://loremflickr.com/1024/768/beer,pub/all?q=" + Guid.NewGuid();

            return Task.FromResult(url);
        }
    }
}
