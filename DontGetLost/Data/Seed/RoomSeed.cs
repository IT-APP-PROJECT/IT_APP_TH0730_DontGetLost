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
                ("01", 65, 670, "", "" ),
                ("02", 130, 670, "", ""),
                ("03", 200, 670, "", ""),
                ("05", 315, 670, "", ""),
                ("06", 400, 670, "", ""),
                ("07", 510, 670, "", ""),
                ("010", 620, 670, "", ""),
                ("011", 700, 670, "", ""),
                ("012", 765, 670, "", ""),
                ("013a", 815, 670, "", ""),
                ("013", 900, 670, "", ""),
                ("014", 1040, 700, "", ""),
                ("015", 1040, 490, "", ""),
                ("016", 880, 490, "", ""),
                ("017", 820, 490, "", ""),
                ("018", 760, 490, "", ""),
                ("019", 630, 490, "", ""),
                ("021", 260, 490, "", ""),
                ("022", 115, 490, "", "")
             });

        private IEnumerable<Room> GetC301Rooms()
          => GenerateRooms(Maps.C301, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC302Rooms()
          => GenerateRooms(Maps.C302, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC303Rooms()
          => GenerateRooms(Maps.C303, new List<(string, int, int, string, string)>());

        private IEnumerable<Room> GetC304Rooms()
          => GenerateRooms(Maps.C304, new List<(string, int, int, string, string)>
          {
              ("301", 65, 690, "", "" ),
                ("302", 120, 690, "", ""),
                ("303", 178, 690, "", ""),
                ("304", 228, 690, "", ""),
                ("305", 280, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=42"),
                ("306", 330, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1016"),
                ("307", 400, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1012"),
                ("308", 505, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=48"),
                ("310", 600, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1028"),
                ("311", 650, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=121"),
                ("312", 700, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=101"),
                ("313", 750, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1009"),
                ("314", 800, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1008"),
                ("315", 850, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1024"),
                ("316", 900, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1010"),
                ("317", 950, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1002"),
                ("317a", 1000, 690, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1195"),
                ("318/318a", 1100, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=50"),
                ("319", 1015, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=46"),
                ("320", 910, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=34"),
                ("320a", 820, 525, "", ""),
                ("321", 770, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=998"),
                ("322", 720, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1029"),
                ("323", 670, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=38"),
                ("324", 620, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1017"),
                ("325", 555, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1018"),
                ("328", 290, 525, "", ""),
                ("329", 230, 525, "", ""),
                ("330", 170, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1015"),
                ("331", 115, 525, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1006"),
                ("332", 60, 525, "", "")
          });

        private IEnumerable<Room> GetC400Rooms()
          => GenerateRooms(Maps.C400, new List<(string, int, int, string, string)>
          {
            ("0.32", 150, 765, "", ""),
            ("0.33", 325, 765, "", ""),
            ("0.34", 600, 765, "", ""),
            ("0.35", 825, 765, "", ""),
            ("0.36", 925, 765, "", ""),
            ("0.38", 995, 465, "", ""),
            ("0.39", 715, 465, "", ""),
            ("0.40", 505, 465, "", ""),
            ("0.41", 225, 465, "", ""),
             });

        private IEnumerable<Room> GetC401Rooms()
          => GenerateRooms(Maps.C401, new List<(string, int, int, string, string)>
             {
                ("31", 240, 820, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=83"),
                ("32", 375, 820, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=7"),
                ("33", 550, 820, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=9"),
                ("34", 685, 820, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1209"),
                ("35", 825, 820, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1139"),
                ("37", 995, 820, "", ""),
                ("39", 900, 570, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=94"),
                ("40", 680, 570, "", ""),
                ("41/42", 365, 570, "", ""),
             });

        private IEnumerable<Room> GetC402Rooms()
          => GenerateRooms(Maps.C402, new List<(string, int, int, string, string)>{
            ("131", 100, 760, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=84" ),
            ("132", 300, 760, " ", "" ),
            ("132b", 445, 760, " ", "" ),
            ("132c", 530, 760, " ", "" ),
            ("133", 625, 760, " ", "" ),
            ("134", 715, 760, " ", "" ),
            ("135", 810, 760, " ", "" ),
            ("136", 900, 760, " ", "" ),
            ("137", 1020, 760, " ", "" ),
            ("138", 1100, 475, " ", "" ),
            ("139a", 910, 475, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=81" ),
            ("139b", 810, 475, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=82" ),
            ("139c", 715, 475, " ", "" ),
            ("140", 480, 525, " ", "" ),
            ("141", 250, 475, " ", "" ),
            ("142", 135, 475, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=80" ),
             });

        private IEnumerable<Room> GetC403Rooms()
          => GenerateRooms(Maps.C403, new List<(string, int, int, string, string)>{
                ("231", 100, 760, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1190" ),
                ("232", 200, 760, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1151" ),
                ("233", 290, 760, " ", "" ),
                ("234", 395, 760, " ", "" ),
                ("235", 480, 760, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1088" ),
                ("236", 575, 760, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1074" ),
                ("237", 665, 760, " ", "" ),
                ("238", 760, 760, " ", "" ),
                ("239", 858, 760, " ", "" ),
                ("240", 940, 760, " ", "" ),
                ("241", 1020, 760, " ", "" ),
                ("243", 1100, 475, " ", "" ),
                ("244", 985, 475, " ", "" ),
                ("245", 855, 475, " ", "" ),
                ("246", 725, 475, " ", "" ),
                ("247", 135, 475, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=128" ),
                ("248", 440, 565, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=67" ),
             });

        private IEnumerable<Room> GetC404Rooms()
          => GenerateRooms(Maps.C404, new List<(string, int, int, string, string)>{
                ("331", 125, 755, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1006" ),
                ("332", 220, 755, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1007" ),
                ("333", 310, 755, "", "" ),
                ("334", 400, 755, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=1225" ),
                ("335", 490, 755, "", "" ),
                ("336", 580, 755, "", "" ),
                ("337", 675, 755, "", "" ),
                ("338", 760, 755, "", "" ),
                ("339", 870, 755, "", "" ),
                ("340", 995, 755, "", "" ),
                ("342", 1070, 480, "", "" ),
                ("344", 785, 565, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=65" ),
                ("345", 550, 480, "lecture hall", "https://prowadzacy.eka.pwr.edu.pl/plansali.php?pole=76" ),
                ("346", 265, 480, "", "" ),
                ("348", 135, 480, "", "" ),
             });
    }
}