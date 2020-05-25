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
          => GenerateRooms(Maps.C303, new List<(string, int, int, string, string)>
              {
                ("301", 65, 690, "Add description", "" ),
                ("302", 120, 690, "Add description", ""),
                ("303", 178, 690, "Add description", ""),
                ("304", 228, 690, "Add description", ""),
                ("305", 280, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=42"),
                ("306", 330, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1016"),
                ("307", 400, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1012"),
                ("308", 505, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=48"),
                ("310", 600, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1028"),
                ("311", 650, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=121"),
                ("312", 700, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=101"),
                ("313", 750, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1009"),
                ("314", 800, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1008"),
                ("315", 850, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1024"),
                ("316", 900, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1010"),
                ("317", 950, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1002"),
                ("317a", 1000, 690, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1195"),
                ("318/318a", 1100, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=50"),
                ("319", 1015, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=46"),
                ("320", 910, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=34"),
                ("320a", 820, 525, "Add description", ""),
                ("321", 770, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=998"),
                ("322", 720, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1029"),
                ("323", 670, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=38"),
                ("324", 620, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1017"),
                ("325", 555, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1018"),
                ("328", 290, 525, "Add description", ""),
                ("329", 230, 525, "Add description", ""),
                ("330", 170, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1015"),
                ("331", 115, 525, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1006"),
                ("332", 60, 525, "Add description", "")
             });

        private IEnumerable<Room> GetC304Rooms()
          => GenerateRooms(Maps.C304, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC400Rooms()
          => GenerateRooms(Maps.C400, new List<(string, int, int, string, string)>     
          {
                ("0.32", 150, 765, "Add description", ""),
                ("0.33", 325, 765, "Add description", ""),
                ("0.34", 600, 765, "Add description", ""),
                ("0.35", 825, 765, "Add description", ""),
                ("0.36", 925, 765, "Add description", ""),
                ("0.38", 995, 605, "Add description", ""),
                ("0.39", 715, 605, "Add description", ""),
                ("0.40", 505, 605, "Add description", ""),
                ("0.41", 225, 605, "Add description", ""),
             });

        private IEnumerable<Room> GetC401Rooms()
          => GenerateRooms(Maps.C401, new List<(string, int, int, string, string)>     
             {
                ("31", 240, 820, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=83"),
                ("32", 325, 820, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=7"),
                ("33", 600, 820, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=9"),
                ("34", 825, 820, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1209"),
                ("35", 925, 820, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1139"),
                ("37", 995, 820, "Add description", ""),
                ("39", 930, 570, "Add description", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=94"),
                ("40", 680, 570, "Add description", ""),
                ("41/42", 365, 570, "Add description", ""),
             });

        private IEnumerable<Room> GetC402Rooms()
          => GenerateRooms(Maps.C402, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC403Rooms()
          => GenerateRooms(Maps.C403, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC404Rooms()
          => GenerateRooms(Maps.C404, new List<(string, int, int, string, string)>());
    }
}