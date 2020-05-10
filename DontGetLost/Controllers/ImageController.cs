using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DontGetLost.Models;
using DontGetLost.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DontGetLost.Controllers
{
    public class ImageController : Controller
    {

        private readonly ILogger<ImageController> _logger;

        private readonly ICloudinaryService _cloudinary;

        public ImageController(ICloudinaryService cloudinary, ILogger<ImageController> logger)
        {
            _logger = logger;
            _cloudinary = cloudinary;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Image/Upload")]
        public ActionResult<CloudinaryData> Upload(string name, string path)
        {
            var clouadinaryData = _cloudinary.uploadImage(name, path);
            _logger.LogInformation(clouadinaryData.ImageName + " : " + clouadinaryData.Url);

            return Ok(clouadinaryData);
        }
        [Route("Image/Download")]
        public ActionResult<CloudinaryData> Download(string imageName)
        {
            var result = _cloudinary.getImage(imageName);
            if (result.Count() == 0)
            {
                _logger.LogError("image fetch has failed");
                return BadRequest();
            }

            return Ok(result);
        }
    }
}