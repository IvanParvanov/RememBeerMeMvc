using System;
using System.IO;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using RememBeer.Common.Configuration;
using RememBeer.Common.Services.Contracts;

namespace RememBeer.Common.Services
{
    public class CloudinaryImageUpload : IImageUploadService
    {
        private readonly Cloudinary cloud;

        public CloudinaryImageUpload(IConfigurationProvider config)
        {
            var name = config.ImageUploadName;
            var key = config.ImageUploadApiKey;
            var secret = config.ImageUploadApiSecret;
            var account = new Account(name, key, secret);
            this.cloud = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(Stream image, int width, int height)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var id = Guid.NewGuid().ToString();
            var imageUploadParams = new ImageUploadParams
                                    {
                                        File = new FileDescription(id, image),
                                        Transformation = new Transformation()
                                            .Width(width)
                                            .Height(height)
                                            .Crop("fit"),
                                        PublicId = id,
                                        Format = "jpg"
                                    };

            ImageUploadResult result = null;
            try
            {
                result = await this.cloud.UploadAsync(imageUploadParams);
            }
            catch (Exception)
            {
                //Log stuff
            }

            return result?.Uri?.AbsoluteUri;
        }
    }
}
