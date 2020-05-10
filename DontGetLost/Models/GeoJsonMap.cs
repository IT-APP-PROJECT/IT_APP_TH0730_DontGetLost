using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Models
{
    public class GeoJsonMap
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public string Json { get; set; }

        public GeoJsonMap(int mapId, string json )
        {
           
            MapId = mapId;
            Json = json;
        }

        public GeoJsonMap()
        {

        }
    }
}
