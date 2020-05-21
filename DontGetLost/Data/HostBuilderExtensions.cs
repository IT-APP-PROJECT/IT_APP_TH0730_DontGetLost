using DontGetLost.Contracts;
using DontGetLost.Data.Seed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DontGetLost.Data
{
    public static class HostBuilderExtensions
    {
        public static IHost InsertSeed<T>(this IHost host, ISeed<T> seed)
        {
            using var scope = host.Services.CreateScope();
            var repository = scope.ServiceProvider.GetService<IRepository<T>>();
            repository.Create(seed.Content);

            return host;
        }
    }
}