using DontGetLost.Constants;
using DontGetLost.Models;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Data.Seed
{
    public class RoomSeed : ISeed<Room>
    {
        public IEnumerable<Room> Content { get; private set; }

        public RoomSeed()
        {
            Content = GetAllRooms();
        }

        private IEnumerable<Room> GetAllRooms()
        {
            var rooms = new List<Room>();
            rooms.AddRange(GetC300Rooms());
            rooms.AddRange(GetC301Rooms());
            rooms.AddRange(GetC302Rooms());
            rooms.AddRange(GetC303Rooms());
            rooms.AddRange(GetC304Rooms());
            rooms.AddRange(GetC400Rooms());
            rooms.AddRange(GetC401Rooms());
            rooms.AddRange(GetC402Rooms());
            rooms.AddRange(GetC403Rooms());
            rooms.AddRange(GetC404Rooms());

            return rooms;
        }

        private IEnumerable<Room> GenerateRooms(string mapName, List<(string name, int x, int y, string desc, string url)> dataTuples)
          => dataTuples.Select(t => new Room(t.name, mapName, new Point(t.x, t.y), t.desc, t.url));

        private IEnumerable<Room> GetC300Rooms()
          => GenerateRooms(Maps.C300, new List<(string, int, int, string, string)>
             {
                ("01", 65, 670, "Add description", "Add url" ),
                ("02", 130, 670, "Add description", "Add url"),
                ("03", 200, 670, "Add description", "Add url"),
                ("05", 315, 670, "Add description", "Add url"),
                ("06", 400, 670, "Add description", "Add url"),
                ("07", 510, 670, "Add description", "Add url"),
                ("010", 620, 670, "Add description", "Add url"),
                ("011", 700, 670, "Add description", "Add url"),
                ("012", 765, 670, "Add description", "Add url"),
                ("013a", 815, 670, "Add description", "Add url"),
                ("013", 900, 670, "Add description", "Add url"),
                ("014", 1040, 700, "Add description", "Add url"),
                ("015", 1040, 490, "Add description", "Add url"),
                ("016", 880, 490, "Add description", "Add url"),
                ("017", 820, 490, "Add description", "Add url"),
                ("018", 760, 490, "Add description", "Add url"),
                ("019", 630, 490, "Add description", "Add url"),
                ("021", 260, 490, "Add description", "Add url"),
                ("022", 115, 490, "Add description", "Add url")
             });

        private IEnumerable<Room> GetC301Rooms()
          => GenerateRooms(Maps.C301, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC302Rooms()
          => GenerateRooms(Maps.C302, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC303Rooms()
          => GenerateRooms(Maps.C303, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC304Rooms()
          => GenerateRooms(Maps.C304, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC400Rooms()
          => GenerateRooms(Maps.C400, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC401Rooms()
          => GenerateRooms(Maps.C401, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC402Rooms()
          => GenerateRooms(Maps.C402, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC403Rooms()
          => GenerateRooms(Maps.C403, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC404Rooms()
          => GenerateRooms(Maps.C404, new List<(string, int, int, string, string)>());
    }
}