
using CSharpFunctionalExtensions;
using System;

namespace DontGetLost.Models
{
    public sealed class Point
    {
        public int X { get;}
        public int Y { get;}
        public Point(int x, int y)
        {

            X = x;
            Y = y; 
        }
    }
}
