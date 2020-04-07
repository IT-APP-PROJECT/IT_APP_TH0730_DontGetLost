using CSharpFunctionalExtensions;
using DontGetLost.Models;
using System.Collections.Generic;

namespace DontGetLost.Services
{
    public interface IIconService
    {
        Result<IEnumerable<Icon>> GetIconsForMap(int mapId);
    }
}