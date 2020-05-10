using DontGetLost.Contracts;
using DontGetLost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace DontGetLost.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IRepository<CloudinaryData> m_cloudinaryRepository;


        private readonly Account account = new Account(
            "do4piuzmh",
            "276793959885363",
            "g_6fn5o1IpHBmAcma_MQkRDZKNE");


        private Cloudinary cloudinary;

        public CloudinaryService(IRepository<CloudinaryData> cloudRepository)
        {
            m_cloudinaryRepository = cloudRepository;
            cloudinary = new Cloudinary(account);
        }

        public CloudinaryData uploadImage(string imageName, string imagePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath)
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            var newImage = new CloudinaryData(imageName, uploadResult.Uri);
            m_cloudinaryRepository.Create(newImage);

            return newImage;

        }
        public IEnumerable<CloudinaryData> getImage(string imageName)
        {
            var restult = m_cloudinaryRepository.FindAll();
            if (restult.IsSuccess)
                return restult.Value;
            else
                return new List<CloudinaryData>();
        }



    }
}
