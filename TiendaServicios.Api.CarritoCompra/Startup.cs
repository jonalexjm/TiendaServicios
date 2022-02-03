using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteService;


namespace TiendaServicios.Api.CarritoCompra
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

            services.AddScoped<ILibroService, LibroService>();
            services.AddControllers();
            services.AddDbContext<CarritoContexto>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("ConexionDatabase"), ServerVersion.AutoDetect(Configuration.GetConnectionString("ConexionDatabase")));
            });

            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TiendaServicios.Api.CarritoCompras", Version = "v1" });
            });
            services.AddHttpClient("libros", config =>
            {
                config.BaseAddress = new Uri(Configuration["services:Libros"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TiendaServicios.Api.CarritoCompras v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
