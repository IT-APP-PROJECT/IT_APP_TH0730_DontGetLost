using System.Collections.Generic;
using System.Linq;
using DontGetLost.Dtos;
using DontGetLost.Models;
using DontGetLost.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontGetLost.Controllers
{
    public class IconController : Controller
    {
        // GET: /<controller>/
        private readonly IIconService m_iconService;

        public IconController(IIconService iconService)
        {
            m_iconService = iconService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("icons")]
        public ActionResult<IEnumerable<Icon>> GetIcons(int mapId)
        {
            var icons = m_iconService.GetIcons(mapId);

            if (icons.IsFailure) return BadRequest();
            if (icons.Value.Count() == 0) return NoContent();

            return Ok(icons.Value);
        }

        [HttpPost]
        [Route("icons")]
        public ActionResult AddIcon(IconDto dto)
        {
            var result = m_iconService.AddIcon(dto);

            if (result.IsFailure) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("icons")]
        public ActionResult DeleteIcon(int iconId)
        {
            var result = m_iconService.DeleteIcon(iconId);

            if (result.IsFailure) return BadRequest();

            return NoContent();
        }
    }
}
