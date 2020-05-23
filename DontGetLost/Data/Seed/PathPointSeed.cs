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
          => GeneratePathPoints(Maps.C402, new List<(int, int)>()
              .Add(MoveAlongsideX(15, 1150, 630))   // Corridor
                 .Add(MoveAlongsideY(630, 740, 210))   // 131
                 .Add(MoveAlongsideY(630, 740, 260))   // 132
                 .Add(MoveAlongsideY(630, 740, 430))   // 132b
                 .Add(MoveAlongsideY(630, 740, 560))   // 132c
                 .Add(MoveAlongsideY(630, 740, 610))   // 133
                 .Add(MoveAlongsideY(630, 740, 750))   // 134
                 .Add(MoveAlongsideY(630, 740, 805))   // 135
                 .Add(MoveAlongsideY(630, 740, 940))   // 136
                 .Add(MoveAlongsideY(630, 740, 995))   // 137
                 .Add(MoveAlongsideY(630, 740, 1115)) // Tiolet

                 .Add(MoveAlongsideY(500, 630, 1115))  // 138
                 .Add(MoveAlongsideY(555, 630, 820))   // 139
                 .Add(MoveAlongsideX(820, 950, 555))   // 139a_1
                 .Add(MoveAlongsideY(490, 555, 950))   // 139a_2
                 .Add(MoveAlongsideY(500, 555, 820))   // 139b
                 .Add(MoveAlongsideY(490, 630, 715))   // 139c
                 .Add(MoveAlongsideY(555, 630, 405))   // 140_1
                 .Add(MoveAlongsideX(405, 510, 555))   // 140_2
                 .Add(MoveAlongsideY(500, 630, 255))   // 141
                 .Add(MoveAlongsideY(500, 630, 70))   // 142
                  );

        private IEnumerable<PathPoint> GetC403PathPoints()
          => GeneratePathPoints(Maps.C403, new List<(int, int)>()
              .Add(MoveAlongsideX(15, 1150, 630))   // Corridor
                 .Add(MoveAlongsideY(630, 730, 80))   // 231
                 .Add(MoveAlongsideY(630, 730, 215))   // 232
                 .Add(MoveAlongsideY(630, 730, 275))   // 233
                 .Add(MoveAlongsideY(630, 730, 400))   // 234
                 .Add(MoveAlongsideY(630, 730, 460))   // 235
                 .Add(MoveAlongsideY(630, 730, 595))   // 236
                 .Add(MoveAlongsideY(630, 730, 640))   // 237
                 .Add(MoveAlongsideY(630, 730, 780))   // 238
                 .Add(MoveAlongsideY(630, 730, 835))   // 239
                 .Add(MoveAlongsideY(630, 730, 960))   // 240
                 .Add(MoveAlongsideY(630, 730, 1010))   // 241
                 .Add(MoveAlongsideY(630, 730, 1100)) // Tiolet

                 .Add(MoveAlongsideY(500, 630, 1100))  // 243
                 .Add(MoveAlongsideY(500, 630, 940))   // 244
                 .Add(MoveAlongsideY(500, 630, 810))   // 245
                 .Add(MoveAlongsideY(500, 630, 750))   // 246
                 .Add(MoveAlongsideY(560, 630, 265))   // 247_1
                 .Add(MoveAlongsideX(265, 445, 560))   // 247_2
                 .Add(MoveAlongsideY(500, 630, 200))   // 248
                  );

        private IEnumerable<PathPoint> GetC404PathPoints()
          => GeneratePathPoints(Maps.C404, new List<(int, int)>()
              .Add(MoveAlongsideX(60, 1130, 620))   // Corridor
                 .Add(MoveAlongsideY(620, 740, 100))   // 331
                 .Add(MoveAlongsideY(620, 740, 240))   // 332
                 .Add(MoveAlongsideY(620, 740, 290))   // 333
                 .Add(MoveAlongsideY(620, 740, 420))   // 334
                 .Add(MoveAlongsideY(620, 740, 475))   // 335
                 .Add(MoveAlongsideY(620, 740, 605))   // 336
                 .Add(MoveAlongsideY(620, 740, 650))   // 337
                 .Add(MoveAlongsideY(620, 740, 785))   // 338
                 .Add(MoveAlongsideY(620, 740, 835))   // 339
                 .Add(MoveAlongsideY(620, 740, 950))   // 340
                 .Add(MoveAlongsideY(620, 740, 1100)) // Tiolet

                 .Add(MoveAlongsideY(500, 620, 1100))  // 342
                 .Add(MoveAlongsideY(565, 620, 785))   // 344
                 .Add(MoveAlongsideY(520, 620, 620))   // 345
                 .Add(MoveAlongsideY(480, 620, 220))   // 346
                 .Add(MoveAlongsideX(150, 220, 540))   // 348
                  );
    }
}