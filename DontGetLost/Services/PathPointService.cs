using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Dtos;
using DontGetLost.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Services
{
    public class PathPointService : IPathPointService
    {
        private readonly IRepository<PathPoint> m_pathPointRepository;

        public PathPointService(IRepository<PathPoint> pathPointRepostitory)
        {
            m_pathPointRepository = pathPointRepostitory;
        }

        public Result AddPathPoint(PathPointDto dto)
            => MapPathPointDtoToPathPoint(dto)
                .Bind(pathPoint => m_pathPointRepository.Create(pathPoint));

        private Result<PathPoint> MapPathPointDtoToPathPoint(PathPointDto dto)
            => Result.Success(new PathPoint(dto.MapName, dto.X, dto.Y));

        public Result DeletePathPoint(int pathPointId)
            => m_pathPointRepository.Delete(pathPointId);

        public Result<IEnumerable<PathPoint>> GetPathPoints(string mapName)
            => m_pathPointRepository
                .FindAll()
                .Map(x => x.Where(pathPoint => pathPoint.MapName == mapName));
    }
}