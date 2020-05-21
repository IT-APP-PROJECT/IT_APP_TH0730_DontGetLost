using DontGetLost.Constants;
using DontGetLost.Models;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Data.Seed
{
    public class IconSeed : ISeed<Icon>
    {
        public IEnumerable<Icon> Content { get; private set; }

        public IconSeed()
        {
            Content = GetAllIcons();
        }

        private IEnumerable<Icon> GetAllIcons()
        {
            var icons = new List<Icon>();
            icons.AddRange(GetC300Icons());
            icons.AddRange(GetC301Icons());
            icons.AddRange(GetC302Icons());
            icons.AddRange(GetC303Icons());
            icons.AddRange(GetC304Icons());
            icons.AddRange(GetC400Icons());
            icons.AddRange(GetC401Icons());
            icons.AddRange(GetC402Icons());
            icons.AddRange(GetC403Icons());
            icons.AddRange(GetC404Icons());

            return icons;
        }

        private IEnumerable<Icon> GenerateIcons(string mapName, List<(int x, int y, IconType type)> dataTuples)
          => dataTuples.Select(t => new Icon(mapName, new Point(t.x, t.y), t.type));

        private IEnumerable<Icon> GetC300Icons()
          => GenerateIcons(Maps.C300, new List<(int, int, IconType)>
          {
              (445, 490, IconType.Toilet),
              (335, 490, IconType.Stairs),
              (475, 550, IconType.Elevator),
              (1160, 670, IconType.Stairs)
          });

        private IEnumerable<Icon> GetC301Icons()
          => GenerateIcons(Maps.C301, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC302Icons()
          => GenerateIcons(Maps.C302, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC303Icons()
          => GenerateIcons(Maps.C303, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC304Icons()
          => GenerateIcons(Maps.C304, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC400Icons()
          => GenerateIcons(Maps.C400, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC401Icons()
          => GenerateIcons(Maps.C401, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC402Icons()
          => GenerateIcons(Maps.C402, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC403Icons()
          => GenerateIcons(Maps.C403, new List<(int, int, IconType)>());

        private IEnumerable<Icon> GetC404Icons()
          => GenerateIcons(Maps.C404, new List<(int, int, IconType)>());

    }
}