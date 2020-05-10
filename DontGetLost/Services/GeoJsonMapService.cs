using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Dtos;
using DontGetLost.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Services
{
    public class GeoJsonMapService: IGeoJsonMapService
    {
        private readonly IRepository<GeoJsonMap> m_geoJsonMapRepository;

        public GeoJsonMapService(IRepository<GeoJsonMap> geoJsonMapRepostitory)
        {
            m_geoJsonMapRepository = geoJsonMapRepostitory;
        }

        public Result AddGeoJsonMap(GeoJsonMapDto dto)
            => MapGeoJsonMapDtoToGeoJsonMap(dto)
                .Bind(geoJsonMap => m_geoJsonMapRepository.Create(geoJsonMap));

        private Result<GeoJsonMap> MapGeoJsonMapDtoToGeoJsonMap(GeoJsonMapDto dto)
            => Result.Success( new GeoJsonMap(dto.MapId, dto.Json));

        public Result DeleteGeoJsonMap(int geoJsonMapId)
            => m_geoJsonMapRepository.Delete(geoJsonMapId);

        public Result<IEnumerable<GeoJsonMap>> GetGeoJsonMaps(int mapId)
            => m_geoJsonMapRepository
                .FindAll()
                .Map(x => x.Where(geoJsonMap => geoJsonMap.MapId == mapId));
    }
}
