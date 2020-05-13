
using LiteDB;
using System;

namespace DontGetLost.Models
{
    
    public class Icon
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public Point Coordinates { get; set; }
        public IconType Type { get; set; }


        public Icon(int mapId, Point point, IconType type)
        {
            Type = type;
            MapId = mapId;
            Coordinates = point;
        }

        public Icon()
        {

        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
