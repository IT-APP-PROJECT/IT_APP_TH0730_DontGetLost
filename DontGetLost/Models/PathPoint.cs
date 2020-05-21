using LiteDB;

namespace DontGetLost.Models
{
    public class PathPoint
    {
        public ObjectId Id { get; set; }
        public string MapName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public PathPoint(string mapName, int x, int y)
        {
            MapName = mapName;
            X = x;
            Y = y;
        }

        public PathPoint()
        {
        }
    }
}