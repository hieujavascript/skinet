using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(
        this UserManager<AppUser> userManager , 
        ClaimsPrincipal claimsPrincipal) 
        {            
            // đây là noi chua email của user hiện tạichung ta tao trong TokenService 
            var email = claimsPrincipal?.Claims?
                        .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            // tra ve User bao gom luon ca Address
            var user = await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            return user;
        }
        public static async Task<AppUser> FindByUserFromEmailClaimsPrinciple( 
            this UserManager<AppUser> userManager , 
            ClaimsPrincipal claimsPrincipal) {
            var email = claimsPrincipal?.Claims?
                        .SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
           var user = await userManager.FindByEmailAsync(email);
            return user;
        }
    }
}