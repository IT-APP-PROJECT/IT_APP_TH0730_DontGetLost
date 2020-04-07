
namespace DontGetLost.Models
{
    public class Icon
    {
        public MapPoint Point { get; }
        public IconType Type { get; }
        public int MapId { get; set; }

        public Icon(MapPoint point, IconType type, int mapId)
        {
            Point = point;
            Type = type;
            MapId = mapId; 
        }
    }

    public enum IconType
    {
        Elevator, 
        Vendor,
        Cloakroom
    }
}
