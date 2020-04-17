
using LiteDB;
using System;

namespace DontGetLost.Models
{
    
    public class Icon
    {
        public int Id { get; set; }
        public Point Point { get; set; }
        public IconType Type { get; set; }
        public int MapId { get; set; }

        public Icon()
        {

        }

        public Icon(Point point, IconType type, int mapId)
        {
            Type = type;
            MapId = mapId;
            Point = point;
        } 
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
