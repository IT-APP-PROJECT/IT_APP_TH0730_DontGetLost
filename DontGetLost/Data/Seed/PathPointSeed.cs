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
                 .Add(MoveAlongsideX(80, 1140, 625))   // Corridor
                 .Add(MoveAlongsideY(625, 670, 85))    // 01
                 .Add(MoveAlongsideY(625, 670, 115))   // 02
                 .Add(MoveAlongsideY(625, 670, 190))   // 03
                 .Add(MoveAlongsideY(625, 670, 330))   // 05
                 .Add(MoveAlongsideY(625, 670, 540))   // 07
                 .Add(MoveAlongsideY(625, 670, 615))   // 010
                 .Add(MoveAlongsideY(625, 670, 720))   // 011
                 .Add(MoveAlongsideY(625, 670, 825))   // 013a
                 .Add(MoveAlongsideY(625, 670, 970))   // 013
                 .Add(MoveAlongsideY(625, 700, 1100))  // 014 (up)
                 .Add(MoveAlongsideX(1060, 1100, 700)) // 014 (left)
                 .Add(MoveAlongsideX(1100, 1160, 640)) // Stairs (right)
                 .Add(MoveAlongsideY(500, 625, 1050))  // 015
                 .Add(MoveAlongsideY(500, 625, 930))   // 016
                 .Add(MoveAlongsideY(500, 625, 820))   // 017
                 .Add(MoveAlongsideY(500, 625, 760))   // 018
                 .Add(MoveAlongsideY(500, 625, 630))   // 019
                 .Add(MoveAlongsideY(500, 540, 475))   // Elevator
                 .Add(MoveAlongsideY(500, 625, 370))   // Stairs
                 .Add(MoveAlongsideY(500, 625, 220))   // 021
                 .Add(MoveAlongsideY(500, 625, 100))   // 022
                  );

        private IEnumerable<PathPoint> GetC301PathPoints()
          => GeneratePathPoints(Maps.C301, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC302PathPoints()
          => GeneratePathPoints(Maps.C302, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC303PathPoints()
          => GeneratePathPoints(Maps.C303,
           new List<(int, int)>()
                 .Add(MoveAlongsideX(50, 1160, 625))   // Corridor
                 .Add(MoveAlongsideY(625, 685, 75))    // 301
                 .Add(MoveAlongsideY(625, 685, 110))   // 302
                 .Add(MoveAlongsideY(625, 685, 185))   // 303
                 .Add(MoveAlongsideY(625, 685, 220))   // 304
                 .Add(MoveAlongsideY(625, 685, 290))   // 305
                 .Add(MoveAlongsideY(625, 685, 325))   // 306
                 .Add(MoveAlongsideY(625, 685, 425))   // 307
                 .Add(MoveAlongsideY(625, 685, 505))   // 308
                 .Add(MoveAlongsideY(625, 685, 625))   // 310
                 .Add(MoveAlongsideY(625, 685, 640))  // 311
                 .Add(MoveAlongsideY(625, 685, 715))   // 312
                 .Add(MoveAlongsideY(625, 685, 745))   // 313
                 .Add(MoveAlongsideY(625, 685, 820))  // 314
                 .Add(MoveAlongsideY(625, 685, 850))   // 315
                 .Add(MoveAlongsideY(625, 685, 920))   // 316
                 .Add(MoveAlongsideY(625, 685, 955))   // 317
                 .Add(MoveAlongsideY(625, 685, 1030))   // 317a

                 .Add(MoveAlongsideY(560, 625, 470))   // Elevator
                 .Add(MoveAlongsideY(510, 625, 395))   // Stairs
                 .Add(MoveAlongsideY(625, 675, 1070))   // Stairs

                 .Add(MoveAlongsideY(505, 625, 1130))   // 318/318a
                 .Add(MoveAlongsideY(505, 625, 1030))   // 319
                 .Add(MoveAlongsideY(505, 625, 925))   // 320
                 .Add(MoveAlongsideY(505, 625, 825))   // 320a
                 .Add(MoveAlongsideY(505, 625, 765))   // 321
                 .Add(MoveAlongsideY(505, 625, 725))   // 322
                 .Add(MoveAlongsideY(505, 625, 665))   // 323
                 .Add(MoveAlongsideY(505, 625, 625))   // 324
                 .Add(MoveAlongsideY(505, 625, 555))   // 325
                 .Add(MoveAlongsideY(505, 625, 285))   // 328
                 .Add(MoveAlongsideY(505, 625, 220))   // 329
                 .Add(MoveAlongsideY(505, 625, 185))   // 330
                 .Add(MoveAlongsideY(505, 625, 110))   // 331
                 .Add(MoveAlongsideY(505, 625, 80))   // 332
                  );

        private IEnumerable<PathPoint> GetC304PathPoints()
          => GeneratePathPoints(Maps.C304, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC400PathPoints()
          => GeneratePathPoints(Maps.C400,
           new List<(int, int)>()
                 .Add(MoveAlongsideX(20, 1180, 625))   // Corridor
                 .Add(MoveAlongsideY(625, 765, 210))    // 0.32
                 .Add(MoveAlongsideY(625, 765, 390))   // 0.33
                 .Add(MoveAlongsideY(625, 765, 445))   // 0.34
                 .Add(MoveAlongsideY(625, 765, 810))   // 0.35
                 .Add(MoveAlongsideY(625, 765, 945))   // 0.36

                 .Add(MoveAlongsideY(465, 605, 945))   // 0.38
                 .Add(MoveAlongsideY(465, 605, 765))   // 0.39
                 .Add(MoveAlongsideY(465, 605, 445))   // 0.40
                 .Add(MoveAlongsideY(465, 605, 390))   // 0.41
                  );

        private IEnumerable<PathPoint> GetC401PathPoints()
          => GeneratePathPoints(Maps.C401,
           new List<(int, int)>()
                 .Add(MoveAlongsideX(140, 1100, 710))   // Corridor
                 .Add(MoveAlongsideY(710, 820, 305))    // 31
                 .Add(MoveAlongsideY(710, 820, 445))   // 32
                 .Add(MoveAlongsideY(710, 820, 585))   // 33
                 .Add(MoveAlongsideY(710, 820, 715))   // 34
                 .Add(MoveAlongsideY(710, 820, 785))   // 35
                 .Add(MoveAlongsideY(710, 820, 1030))   // 37

                 .Add(MoveAlongsideY(575, 710, 825))   // 39
                 .Add(MoveAlongsideY(575, 710, 765))   // 40
                 .Add(MoveAlongsideY(575, 710, 195))   // 41/42
                  );

        private IEnumerable<PathPoint> GetC402PathPoints()
          => GeneratePathPoints(Maps.C402, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC403PathPoints()
          => GeneratePathPoints(Maps.C403, new List<(int, int)>());

        private IEnumerable<PathPoint> GetC404PathPoints()
          => GeneratePathPoints(Maps.C404, new List<(int, int)>());
    }
}