using DontGetLost.Contracts;
using DontGetLost.Models;
using System;
using System.Collections.Generic;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace DontGetLost.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IRepository<Image> m_cloudinaryRepository;


        private readonly Account account = new Account(
            "do4piuzmh",
            "276793959885363",
            "g_6fn5o1IpHBmAcma_MQkRDZKNE");


        private Cloudinary cloudinary;

        public CloudinaryService(IRepository<Image> cloudRepository)
        {
            m_cloudinaryRepository = cloudRepository;
            cloudinary = new Cloudinary(account);
        }

        public Image uploadImage(string imageName, string imagePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath)
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            var newImage = new Image(imageName, uploadResult.Uri);
            m_cloudinaryRepository.Create(newImage);

            return newImage;

        }
        public IEnumerable<Image> getImage(string imageName)
        {
            var result = m_cloudinaryRepository.FindAll();
            if (result.IsSuccess)
                return result.Value;
            else
                return new List<Image>();
        }



    }
}
