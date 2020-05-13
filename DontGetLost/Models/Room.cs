namespace DontGetLost.Models
{
    public sealed class Room
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public Point Coordinates { get; set; }
        public string Description { get; set; }
        public Room(int mapId, Point coordinates, string description)
        {
            MapId = mapId;
            Coordinates = coordinates;
            Description = description;
        }
        public Room()
        {

        }
    }
}
