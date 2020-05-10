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

        [HttpPost]
        [Route("images")]
        public ActionResult<Image> Upload(string name, string path)
        {
            var clouadinaryData = _cloudinary.uploadImage(name, path);
            _logger.LogInformation(clouadinaryData.Name + " : " + clouadinaryData.Url);

            return Ok(clouadinaryData);
        }
        [HttpGet]
        [Route("images")]
        public ActionResult<Image> Download(string name)
        {
            var result = _cloudinary.getImage(name);
            if (result.Count() == 0)
            {
                _logger.LogError("image fetch has failed");
                return BadRequest();
            }

            return Ok(result);
        }
    }
}