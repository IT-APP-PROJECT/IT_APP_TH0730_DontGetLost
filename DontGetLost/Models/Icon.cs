using LiteDB;

namespace DontGetLost.Models
{
    public class Icon
    {
        public ObjectId Id { get; set; }
        public string MapName { get; set; }
        public Point Coordinates { get; set; }
        public IconType Type { get; set; }

        public Icon(string mapName, Point point, IconType type)
        {
            Type = type;
            MapName = mapName;
            Coordinates = point;
        }

        public Icon()
        {
        }
    }
}