
using System.Data;
using System.Linq;
using API.Error;
using API.Extensions;
using API.helper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        // IConfiguration : sẽ gọi key và value của appsetting.development
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // add các DependencyInjector
        public void ConfigureServices(IServiceCollection services)
        {

          
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddApplicationServices();
            services.AddSwaggerDocumentation(); // test như là posman
             services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // chúng ta thêm các Middleware muốn làm gì đó trước khi gửi Request
        // như là Authencation , UseAuthorization
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            // "/errors/{0}" chinh la [Route("errors/{statusCode}")] trogn error Controller
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection(); // đây là 1 middleware
            app.UseRouting();
            app.UseStaticFiles(); // static file
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseSwaggerDocumentation(); // extension
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
