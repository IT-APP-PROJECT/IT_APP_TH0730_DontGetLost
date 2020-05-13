using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Dtos;
using DontGetLost.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Services
{
    public class RoomService: IRoomService
    {
        private readonly IRepository<Room> m_roomRepository;

        public RoomService(IRepository<Room> roomRepostitory)
        {
            m_roomRepository = roomRepostitory;
        }

        public Result AddRoom(RoomDto dto)
            => MapRoomDtoToRoom(dto)
                .Bind(icon => m_roomRepository.Create(icon));

        private Result<Room> MapRoomDtoToRoom(RoomDto dto)
            => Result.Success( new Room(dto.MapId, new Point(dto.X, dto.Y), dto.Description));

        public Result DeleteRoom(int iconId)
            => m_roomRepository.Delete(iconId);

        public Result<IEnumerable<Room>> GetRooms(int mapId)
            => m_roomRepository
                .FindAll()
                .Map(x => x.Where(icon => icon.MapId == mapId));
    }
}
