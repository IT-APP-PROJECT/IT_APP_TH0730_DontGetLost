using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Dtos;
using DontGetLost.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> m_roomRepository;

        public RoomService(IRepository<Room> roomRepostitory)
        {
            m_roomRepository = roomRepostitory;
        }

        public Result AddRoom(RoomDto dto)
            => MapRoomDtoToRoom(dto)
                .Bind(room => m_roomRepository.Create(room));

        private Result<Room> MapRoomDtoToRoom(RoomDto dto)
            => Result.Success(new Room(dto.Name, dto.MapName, new Point(dto.X, dto.Y), dto.Description, dto.Url));

        public Result DeleteRoom(int roomId)
            => m_roomRepository.Delete(roomId);

        public Result<IEnumerable<Room>> GetRooms(string mapName)
            => m_roomRepository
                .FindAll()
                .Map(x => x.Where(room => room.MapName == mapName));
    }
}