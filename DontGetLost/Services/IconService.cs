

using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using DontGetLost.Models;
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

        public Result<IEnumerable<Icon>> GetIconsForMap(int mapId)
            => m_iconRepository
                .FindAll()
                .Map(x => x.Where(icon => icon.MapId == mapId));

    }
}
