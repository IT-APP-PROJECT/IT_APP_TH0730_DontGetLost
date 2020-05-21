using CSharpFunctionalExtensions;
using DontGetLost.Models;

namespace DontGetLost.Services
{
    public interface ICloudinaryService
    {
        public Image uploadImage(string imageName, string imagePath);

        public Result<Image> getImage(string imageName);
    }
}