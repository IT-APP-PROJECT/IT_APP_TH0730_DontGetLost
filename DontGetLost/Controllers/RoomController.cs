using DontGetLost.Dtos;
using DontGetLost.Models;
using DontGetLost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DontGetLost.Controllers
{
    public class RoomController : Controller
    {
        // GET: /<controller>/
        private readonly IRoomService m_roomService;

        public RoomController(IRoomService roomService)
        {
            m_roomService = roomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("rooms")]
        public ActionResult<IEnumerable<Room>> GetRooms(string mapName)
        {
            var rooms = m_roomService.GetRooms(mapName);

            if (rooms.IsFailure) return BadRequest();
            if (rooms.Value.Count() == 0) return NoContent();

            return Ok(rooms.Value);
        }

        [HttpPost]
        [Route("rooms")]
        public ActionResult AddRoom(RoomDto dto)
        {
            var result = m_roomService.AddRoom(dto);

            if (result.IsFailure) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("rooms")]
        public ActionResult DeleteRoom(int roomId)
        {
            var result = m_roomService.DeleteRoom(roomId);

            if (result.IsFailure) return BadRequest();

            return NoContent();
        }
    }
}