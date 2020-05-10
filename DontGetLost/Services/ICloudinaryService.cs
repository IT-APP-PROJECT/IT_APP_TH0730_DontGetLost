using DontGetLost.Contracts;
using DontGetLost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Services
{
    public interface ICloudinaryService
    {
        public CloudinaryData uploadImage(string imageName, string imagePath);
        public IEnumerable<CloudinaryData> getImage(string imageName);
    }
}
