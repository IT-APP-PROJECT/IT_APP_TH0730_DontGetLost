using DontGetLost.Models;

namespace DontGetLost.Dtos
{
    public class RoomDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Description { get; }
        public int MapId { get; set; }

    }
}
