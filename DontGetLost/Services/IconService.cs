using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Dtos;
using DontGetLost.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontGetLost.Services
{
    public class IconService : IIconService
    {
        private readonly IRepository<Icon> m_iconRepository;

        public IconService(IRepository<Icon> iconRepository)
        {
            m_iconRepository = iconRepository;
        }

        public Result AddIcon(IconDto dto)
            => MapIconDtoToIcon(dto)
                .Bind(icon => m_iconRepository.Create(icon));

        private Result<Icon> MapIconDtoToIcon(IconDto dto)
            => Result.Success(new Icon(dto.MapId, new Point(dto.X, dto.Y), dto.Type));

        public Result DeleteIcon(int iconId)
            => m_iconRepository.Delete(iconId);

        public Result<IEnumerable<Icon>> GetIcons(int mapId)
            => m_iconRepository
                .FindAll()
                .Map(x => x.Where(icon => icon.MapId == mapId));
    }
}
