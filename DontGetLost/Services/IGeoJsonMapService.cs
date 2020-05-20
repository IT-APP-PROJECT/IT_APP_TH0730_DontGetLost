using CSharpFunctionalExtensions;
using DontGetLost.Dtos;
using DontGetLost.Models;
using System.Collections.Generic;

namespace DontGetLost.Services
{
    public interface IPathPointService
    {
        Result<IEnumerable<PathPoint>> GetPathPoints(string mapName);

        Result DeletePathPoint(int pathPointId);

        Result AddPathPoint(PathPointDto dto);
    }
}