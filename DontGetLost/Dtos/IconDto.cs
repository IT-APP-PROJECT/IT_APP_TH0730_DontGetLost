using DontGetLost.Models;

namespace DontGetLost.Dtos
{
    public class IconDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public IconType Type { get; }
        public int MapId { get; set; }

    }
}
