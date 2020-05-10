using CSharpFunctionalExtensions;
using DontGetLost.Dtos;
using DontGetLost.Models;
using System.Collections.Generic;

namespace DontGetLost.Services
{
    public interface IRoomService
    {
        Result<IEnumerable<Room>> GetRooms(int mapId);
        Result DeleteRoom(int roomId);
        Result AddRoom(RoomDto dto);
    }
}