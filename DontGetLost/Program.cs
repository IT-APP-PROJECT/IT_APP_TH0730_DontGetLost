using DontGetLost.Constants;
using DontGetLost.Data;
using DontGetLost.Data.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DontGetLost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ClearDatabase();

            CreateHostBuilder(args)
                .Build()
                .InsertSeed(new ImageSeed())
                .InsertSeed(new RoomSeed())
                .InsertSeed(new IconSeed())
                .InsertSeed(new PathPointSeed())
                .Run();
        }
        public static void ClearDatabase()
        {
            if (File.Exists(DatabasePaths.Data))
            {
                File.Delete(DatabasePaths.Data);
            }

            if (File.Exists(DatabasePaths.Log))
            {
                File.Delete(DatabasePaths.Log);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}