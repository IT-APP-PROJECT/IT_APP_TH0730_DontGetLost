using CSharpFunctionalExtensions;
using DontGetLost.Dtos;
using DontGetLost.Models;
using System.Collections.Generic;

namespace DontGetLost.Services
{
    public interface IIconService
    {
        Result<IEnumerable<Icon>> GetIcons(int mapId);
        Result DeleteIcon(int iconId);
        Result AddIcon(IconDto dto);
    }
}