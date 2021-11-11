using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core.Entity.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
         private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config) // lay key tu appsettting.json
        {
            this._config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Token:Key"]));
        }

        // lay key dinh nghia trong appsetting.json 

        public string CreateToken(AppUser appUser)
        {
            // tao Claim vi Token no nhan vao 1 ClaimIdentity<Claim>
            // có thể truy xuất bằng HttpContext.Use?.Claim()
            var claim = new List<Claim>();
                claim.Add(new Claim(ClaimTypes.Email , appUser.Email));
                claim.Add(new Claim(ClaimTypes.GivenName ,appUser.DisplayName));
               // claim.Add(new Claim(ClaimTypes.Name , appUser.DisplayName));
            // tao Credential de ma hoa key bang thuat toan
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            //khai bao noi dung trong token   
            var tokenDescriptor = new SecurityTokenDescriptor{
                Issuer = _config["Token:Issuer"] , 
                Subject = new ClaimsIdentity(claim),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(7)
            };
             var token_handler = new JwtSecurityTokenHandler(); // cai tu nuget hoac chep vao Infrastructure.csproj 

             var token = token_handler.CreateToken(tokenDescriptor);
             // show token ra 
             return token_handler.WriteToken(token);
        }
    }
}