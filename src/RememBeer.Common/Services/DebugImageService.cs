using System;

using RememBeer.Common.Services.Contracts;

namespace RememBeer.Common.Services
{
    public class DebugImageService : IImageUploadService
    {
        public string UploadImage(byte[] image, int width, int height)
        {
            return "http://loremflickr.com/1024/768/beer,pub/all?q=" + Guid.NewGuid();
        }
    }
}
