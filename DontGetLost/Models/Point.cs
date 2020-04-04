using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Models
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y; 
        }
    }
}
