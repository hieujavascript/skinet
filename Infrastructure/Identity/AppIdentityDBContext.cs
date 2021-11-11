using Core.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Identity
{    
   //Tạo Context thì phải vào StartUp.cs để khai báo service.AddContext<Ten_DBcontext>
    // IdentityDbContext<AppUser> them App User thi no se them nhung Property cua Appiser vao bang AspNetUser
    // dung cho Migrations sinh ra bang
    public class AppIdentityDBContext  : IdentityDbContext<AppUser> // sinh ra cho chúng ta những bảng dùng để xác thực
    {
        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options) : base(options) // base(option) là tham số của conntructor IdentityDbContext
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}