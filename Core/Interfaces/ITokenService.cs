using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity.Identity;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        // dùng AppUser để truyền emal vào làm nội dung hiện thị của token 
        public string CreateToken(AppUser appUser);
    }
}