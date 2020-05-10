using System.Collections.Generic;
using System.Linq;
using DontGetLost.Dtos;
using DontGetLost.Models;
using DontGetLost.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontGetLost.Controllers
{
    public class GeoJsonMapController : Controller
    {
        // GET: /<controller>/
        private readonly IGeoJsonMapService m_geoJsonMapService;

        public GeoJsonMapController(IGeoJsonMapService geoJsonMapService)
        {
            m_geoJsonMapService = geoJsonMapService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("geoJsonMaps")]
        public ActionResult<IEnumerable<GeoJsonMap>> GetGeoJsonMaps(int mapId)
        {
            var geoJsonMaps = m_geoJsonMapService.GetGeoJsonMaps(mapId);

            if (geoJsonMaps.IsFailure) return BadRequest();
            if (geoJsonMaps.Value.Count() == 0) return NoContent();

            return Ok(geoJsonMaps.Value);
        }

        [HttpPost]
        [Route("geoJsonMaps")]
        public ActionResult AddGeoJsonMap(GeoJsonMapDto dto)
        {
            var result = m_geoJsonMapService.AddGeoJsonMap(dto);

            if (result.IsFailure) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("geoJsonMaps")]
        public ActionResult DeleteGeoJsonMap(int geoJsonMapId)
        {
            var result = m_geoJsonMapService.DeleteGeoJsonMap(geoJsonMapId);

            if (result.IsFailure) return BadRequest();

            return NoContent();
        }
    }
}
