using Microsoft.AspNetCore.Identity;

namespace Core.Entity.Identity
{
    // phai tao ra  extension AddIdentityService  để add vào service StartUp.cs
    public class AppUser : IdentityUser // ĐỂ có sẵn PasswordHasd ,PasswordSalt , UserName , email v....v
    {
        // đây là những property mà chúng ta muốn thêm vào ASP_USER_Identitt
        public string DisplayName { get; set; }
        public Address Address { get; set; }
       
    }
}