using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Icon>> GetIconsForMap(int mapId)
        { 
            var icons = m_iconService.GetIconsForMap(mapId);

            if (icons.IsFailure) return NotFound();

            return Ok(icons.Value);
        }

    }
}
