
using System.Text;
using Core.Entity.Identity;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions // Add Identity tới StartUp.cs
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection service , IConfiguration config) {
            
            
            // add AddIdentityCore để sử dụng Identity cho addSign ,add role v...v
            var build = service.AddIdentityCore<AppUser>(opt => {
                // password đơn giản
               // opt.Password.RequireNonAlphanumeric = false;
            });
            build = new IdentityBuilder(build.UserType , build.Services);
            build.AddEntityFrameworkStores<AppIdentityDBContext>();
            build.AddSignInManager<SignInManager<AppUser>>(); // sử dụng SignIn Mamager
            // thêm authentication
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = false,
                        ValidateAudience = false 
                    };
                });
            return service;
        }
    }
}