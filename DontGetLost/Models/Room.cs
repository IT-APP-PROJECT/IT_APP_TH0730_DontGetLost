using LiteDB;

namespace DontGetLost.Models
{
    public class Room
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string MapName { get; set; }
        public Point Coordinates { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public Room(string name, string mapName, Point coordinates, string description, string url)
        {
            Name = name;
            MapName = mapName;
            Coordinates = coordinates;
            Description = description;
            Url = url;
        }

        public Room()
        {
        }
    }
}