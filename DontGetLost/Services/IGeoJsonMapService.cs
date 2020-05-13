using CSharpFunctionalExtensions;
using DontGetLost.Dtos;
using DontGetLost.Models;
using System.Collections.Generic;

namespace DontGetLost.Services
{
    public interface IGeoJsonMapService
    {
        Result<IEnumerable<GeoJsonMap>> GetGeoJsonMaps(int mapId);
        Result DeleteGeoJsonMap(int geoJsonMapId);
        Result AddGeoJsonMap(GeoJsonMapDto dto);
    }
}