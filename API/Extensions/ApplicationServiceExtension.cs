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
              // ================ Start ========= hien thi loi 400 chi tiet cho Client de kiem soat
            services.Configure<ApiBehaviorOptions>(options => {
               options.InvalidModelStateResponseFactory = actionContext => {
                   var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse {
                         Errors = errors
                    };
                return new BadRequestObjectResult(errorResponse);
               };
            });
        //  ===========================  end  ======================================
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            return services;
        }
    }
}