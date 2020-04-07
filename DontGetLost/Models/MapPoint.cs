
using CSharpFunctionalExtensions;
using System;

namespace DontGetLost.Models
{
    public sealed class MapPoint
    {
        public int X { get;}
        public int Y { get;}
        private MapPoint(int x, int y)
        {

            X = x;
            Y = y; 
        }

        public static Result<MapPoint> Create(int x, int y)
        { 
            if (x < 0) return Result.Failure<MapPoint>("X shouln't be less than 0");
            if (y < 0) return Result.Failure<MapPoint>("Y shouln't be less than 0");

            return Result.Success(new MapPoint(x, y));

        }
    }
}
