using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontGetLost.Models
{
    public class Icon
    {
        public Point Point { get; }

        public Icon(Point point)
        {
            Point = point; 
        }
    }
}
