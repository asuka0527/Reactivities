using Application.Ativities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions
{
    // we need to make this static so we dont need to instanciate to be able to use our extensions
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

            // CORS 
            services.AddCors(opt =>{
                opt.AddPolicy("CorsPolicy", policy =>
                 {
                   policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");     
                });
            });

            // MEDIATOR - tell where are handlers located and which assembly are the located
            services.AddMediatR(typeof(List.Handler).Assembly);

            // AUTOMAPPER
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}