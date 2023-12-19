﻿using Application.Handlers.Logic;
using Application.Services;
using Domain.Models.Tables;
using Domain.Repositories.EFInitial;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.Extensions
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey:SymmetricSecurityKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SellersOnly", policy => policy.RequireClaim("SellerId"));
                options.AddPolicy("ModeratorsOnly", policy => policy.RequireClaim("ModeratorId"));
                options.AddPolicy("CustomersOnly", policy => policy.RequireClaim("CustomerId"));
            });

            services.AddScoped<TokenService>();

            services.AddScoped<ILogin, Login>();
            services.AddScoped<IRegister, Register>();
            services.AddScoped<IUserHandler, UserHandler>();

            return services;
        }
    }
}
