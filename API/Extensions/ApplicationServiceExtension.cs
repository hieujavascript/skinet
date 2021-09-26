using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Error;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddScoped<IRepositoryProduct , RepositoryProduct>();
            services.AddScoped((typeof(IGenericRepository<>))  , (typeof(GenericRepository<>)));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            return services;
        }
    }
}