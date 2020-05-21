using DontGetLost.Constants;
using DontGetLost.Models;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Data.Seed
{
    public class PathPointSeed : ISeed<PathPoint>
    {
        public IEnumerable<PathPoint> Content { get; private set; }

        public PathPointSeed()
        {
            Content = GetAllPathPoints();
        }

        private IEnumerable<PathPoint> GetAllPathPoints()
        {
            var pathPoints = new List<PathPoint>();
            pathPoints.AddRange(GetC300PathPoints());
            pathPoints.AddRange(GetC301PathPoints());
            pathPoints.AddRange(GetC302PathPoints());
            pathPoints.AddRange(GetC303PathPoints());
            pathPoints.AddRange(GetC304PathPoints());
            pathPoints.AddRange(GetC400PathPoints());
            pathPoints.AddRange(GetC401PathPoints());
            pathPoints.AddRange(GetC402PathPoints());
            pathPoints.AddRange(GetC403PathPoints());
            pathPoints.AddRange(GetC404PathPoints());

            return pathPoints;
        }

        private IEnumerable<PathPoint> GeneratePathPoints(string mapName, List<(int x, int y)> dataTuples)
          => dataTuples.Select(t => new PathPoint(mapName, t.x, t.y));

        /// <summary>
        /// Generates points between two X values in increments of 5
        /// Example: MoveAlongsideX(5, 15, 10) => ((5, 15, 10), (10,15,10), (15,15,10))
        /// </summary>
        private IEnumerable<(int x, int y)> MoveAlongsideX(int startingX, int endingX, int yValue)
            => Enumerable
                .Range(startingX, System.Math.Abs(endingX - startingX)) //Generate value from startingX to endingX
                .Where((x, i) => i % 5 == 0) //Select every fifth generated value
                .Select(x => (x, yValue)); // Turn into tuple

        /// <summary>
        /// Same as above just alongside Y axis
        /// </summary>
        private IEnumerable<(int x, int y)> MoveAlongsideY(int startingY, int endingY, int xValue)
          => Enumerable
              .Range(startingY, System.Math.Abs(endingY - startingY))
              .Where((x, i) => i % 5 == 0)
              .Select(y => (xValue, y));

        private IEnumerable<PathPoint> GetC300PathPoints()
          => GeneratePathPoints(Maps.C300,
               new List<(int, int)>()
                 .Add(MoveAlongsideX(80, 1140, 600))   // Corridor
                 .Add(MoveAlongsideY(600, 670, 85))    // 01
                 .Add(MoveAlongsideY(600, 670, 115))   // 02
                 .Add(MoveAlongsideY(600, 670, 190))   // 03
                 .Add(MoveAlongsideY(600, 670, 330))   // 05
                 .Add(MoveAlongsideY(600, 670, 540))   // 07
                 .Add(MoveAlongsideY(600, 670, 615))   // 010
                 .Add(MoveAlongsideY(600, 670, 720))   // 011
                 .Add(MoveAlongsideY(600, 670, 825))   // 013a
                 .Add(MoveAlongsideY(600, 670, 970))   // 013
                 .Add(MoveAlongsideY(600, 700, 1100))  // 014 (up)
                 .Add(MoveAlongsideX(1060, 1100, 700)) // 014 (left)
                 .Add(MoveAlongsideX(1100, 1160, 640)) // Stairs (right)
                 .Add(MoveAlongsideY(500, 600, 1050))  // 015
                 .Add(MoveAlongsideY(500, 600, 930))   // 016
                 .Add(MoveAlongsideY(500, 600, 820))   // 017
                 .Add(MoveAlongsideY(500, 600, 760))   // 018
                 .Add(MoveAlongsideY(500, 600, 630))   // 019
                 .Add(MoveAlongsideY(500, 540, 475))   // Elevator
                 .Add(MoveAlongsideY(500, 600, 370))   // Stairs
                 .Add(MoveAlongsideY(500, 600, 220))   // 021
                 .Add(MoveAlongsideY(500, 600, 100))   // 022
                  );

        private IEnumerable<PathPoint> GetC301PathPoints()
          => GeneratePathPoints(Maps.C301, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC302PathPoints()
          => GeneratePathPoints(Maps.C302, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC303PathPoints()
          => GeneratePathPoints(Maps.C303, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC304PathPoints()
          => GeneratePathPoints(Maps.C304, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC400PathPoints()
          => GeneratePathPoints(Maps.C400, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC401PathPoints()
          => GeneratePathPoints(Maps.C401, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC402PathPoints()
          => GeneratePathPoints(Maps.C402, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC403PathPoints()
          => GeneratePathPoints(Maps.C403, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC404PathPoints()
          => GeneratePathPoints(Maps.C404, new List<(int, int)>());
    }
}