using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Error;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) {

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
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation (this IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json" , "Skinet Api V1");
            });
            return app;
        }
    }
}