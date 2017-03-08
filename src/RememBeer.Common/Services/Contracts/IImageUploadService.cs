namespace RememBeer.Common.Services.Contracts
{
    public interface IImageUploadService
    {
        string UploadImage(byte[] image, int width, int height);
    }
}
