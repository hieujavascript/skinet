using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity // để sử dụng Identity service chúng ta phải thêm nó vào Configure service StartUp
{
    public class AppIdentityUserContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager) {
            // neu ko co bat ky user nao trong database
            if(!userManager.Users.Any()){
                var appUser = new AppUser{
                    DisplayName = "hieu", // của class AppUser do chúng ta khai báo
                    Email = "hieu@gmail.com" , // có sẵn khi chúng ta kế thừa IdentityUser nen se co cac thuoc tinh nay du ko khai bao
                    UserName = "hieu@gmail.com", // có sẵn khi kế thừa
                    Address = new Address { // Address la mot bang khac quan he 1 - 1 voi bang AppUser do ta khai báo
                        FirstName = "tran ngoc",
                        LastName = "hieu",
                        Street = "Tan Thuan Tay",
                        City = "New York",
                        State = "YA",
                        ZipCode = "90210" // zip code New York
                    }
                };
                await userManager.CreateAsync(appUser , "123789@Asd"); // 166 bài userIdentity
            }
        } 
    }
}
