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

        private IEnumerable<Room> GenerateRooms(string mapName, List<(string name, int x, int y, string desc)> dataTuples)
          => dataTuples.Select(t => new Room(t.name, mapName, new Point(t.x, t.y), t.desc));

        private IEnumerable<Room> GetC300Rooms()
          => GenerateRooms(Maps.C300, new List<(string, int, int, string)>
             {
                ("01", 65, 670, "Add description"),
                ("02", 130, 670, "Add description"),
                ("03", 200, 670, "Add description"),
                ("05", 315, 670, "Add description"),
                ("06", 400, 670, "Add description"),
                ("07", 510, 670, "Add description"),
                ("010", 620, 670, "Add description"),
                ("011", 700, 670, "Add description"),
                ("012", 765, 670, "Add description"),
                ("013a", 815, 670, "Add description"),
                ("013", 900, 670, "Add description"),
                ("014", 1040, 700, "Add description"),
                ("015", 1040, 490, "Add description"),
                ("016", 880, 490, "Add description"),
                ("017", 820, 490, "Add description"),
                ("018", 760, 490, "Add description"),
                ("019", 630, 490, "Add description"),
                ("021", 260, 490, "Add description"),
                ("022", 115, 490, "Add description")
             });

        private IEnumerable<Room> GetC301Rooms()
          => GenerateRooms(Maps.C301, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC302Rooms()
          => GenerateRooms(Maps.C302, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC303Rooms()
          => GenerateRooms(Maps.C303, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC304Rooms()
          => GenerateRooms(Maps.C304, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC400Rooms()
          => GenerateRooms(Maps.C400, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC401Rooms()
          => GenerateRooms(Maps.C401, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC402Rooms()
          => GenerateRooms(Maps.C402, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC403Rooms()
          => GenerateRooms(Maps.C403, new List<(string, int, int, string)>());

        private IEnumerable<Room> GetC404Rooms()
          => GenerateRooms(Maps.C404, new List<(string, int, int, string)>());
    }
}