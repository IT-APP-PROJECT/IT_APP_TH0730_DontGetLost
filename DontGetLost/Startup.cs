using DontGetLost.Contracts;
using DontGetLost.Models;
using DontGetLost.Repository;
using DontGetLost.Services;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Web.Http;
using Microsoft.OpenApi.Models;
using DontGetLost.Services;

namespace DontGetLost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new LiteDatabase(@"Data\Database.db"));
            services.AddSingleton<IRepository<Icon>, Repository<Icon>>();
            services.AddSingleton<IRepository<Image>, Repository<Image>>();
            services.AddSingleton<IRepository<Room>, Repository<Room>>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IIconService, IconService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
