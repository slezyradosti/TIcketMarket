using Application.Core;
using Application.DTOs.Users.HTTP;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.OpenApi.Models;
using Domain.Repositories.Repos.Interfaces.Catalogues;
using Domain.Repositories.Repos.Catalogues;
using Domain.Repositories.Repos.Tables;

namespace webapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(cfg =>
                {
                    cfg.WithOrigins(configuration["AllowedOrigins"]);
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyMethod();
                });
                options.AddPolicy(name: "AnyOrigin", cfg =>
                {
                    cfg.AllowAnyOrigin();
                    cfg.AllowAnyHeader();
                    cfg.AllowAnyMethod();
                });
            });

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(List.Handler).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddScoped<IEventCategoryRepository, EventCategoryRepository>();
            services.AddScoped<IEventTableRepository, EventTableRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITableEventRepository, TableEventRepository>();
            services.AddScoped<ITicketOrderRepository, TicketOrderRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            //services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

            return services;
        }
    }
}
