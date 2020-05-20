using DontGetLost.Dtos;
using DontGetLost.Models;
using DontGetLost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontGetLost.Controllers
{
    public class PathPointController : Controller
    {
        // GET: /<controller>/
        private readonly IPathPointService m_pathPointService;

        public PathPointController(IPathPointService pathPointService)
        {
            m_pathPointService = pathPointService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("pathPoints")]
        public ActionResult<IEnumerable<PathPoint>> GetPathPoints(string mapName)
        {
            var pathPoints = m_pathPointService.GetPathPoints(mapName);

            if (pathPoints.IsFailure) return BadRequest();
            if (pathPoints.Value.Count() == 0) return NoContent();

            return Ok(pathPoints.Value);
        }

        [HttpPost]
        [Route("pathPoints")]
        public ActionResult AddPathPoint(PathPointDto dto)
        {
            var result = m_pathPointService.AddPathPoint(dto);

            if (result.IsFailure) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("pathPoints")]
        public ActionResult DeletePathPoint(int pathPointId)
        {
            var result = m_pathPointService.DeletePathPoint(pathPointId);

            if (result.IsFailure) return BadRequest();

            return NoContent();
        }
    }
}