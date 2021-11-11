using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dto;
using API.Error;
using API.Extensions;
using AutoMapper;
using Core.Entity.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        //UserManager<AppUser> là tham số trong AppIdentityUserContextSeed
        //  SignInManager<AppUser> tạo trong IdentityServiceExtensions 
        public AccountController(UserManager<AppUser> userManager , 
        SignInManager<AppUser> signInManager , ITokenService token , 
        IMapper mapper
        )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._token = token;
            this._mapper = mapper;
        }
        //do cai dat authencation = token , nen muon goi phuong thuc Get phai yeu cau co Token truyen tu Client len
        [Authorize] 
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUsers() {
            // chung ta da co Claim
            var email =  HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //  lay ra user từ Email
            var user =  await _userManager.FindByEmailAsync(email);
            return new UserDto {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _token.CreateToken(user)
            };
        }
       
         [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            // neu ko la null se tra ve true
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize] // yêu cầu phải đăng nhập và ta sẽ có ClaimsPrincipal chứa email người đăng nhập
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> getAddress() {
                   
             // Address nó chưa Appuser và AppUser chứa Address 
             // điều này dẫn đến lỗi do nó cố gắng trả dữ liệu qua lại giữa AppUser và Address
             // cho nên ta phải dùng AddressDto , loại bỏ đi AppUser trong đó
            //return user.Address;
           var user = await _userManager.FindByEmailWithAddressAsync(HttpContext.User);
           return this._mapper.Map<Address , AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto){
            var user = await _userManager.FindByEmailWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto , Address>(addressDto);
            // da update ca AppUser bao gom luon Address
            var result = await _userManager.UpdateAsync(user);
            // gio Map tu Address qua AddressDto de hien thi
            if(result.Succeeded)
            return Ok(_mapper.Map<Address ,AddressDto>(user.Address));

            return BadRequest("Problem update Address");
        }
        [Authorize]
        [HttpPut("updatePassword")] 
        public async Task<ActionResult<AppUser>> UpdatePassword (LoginDto loginDto) {
            // HttpContext.User?.Claims? chinh la ClaimsPrincipal
            var email = HttpContext.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            
            var newPassword = _userManager.PasswordHasher.HashPassword(user,loginDto.Password);
            user.PasswordHash = newPassword;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            return Ok(user);
            
            else return BadRequest("Problem Update Email") ;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
            // user tra ve rat nhieu thogn tin nen ta dung UserDto
            // layy ra user tu Usermanager<Appuser>
            var user = await this._userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
            if(user == null)
            return Unauthorized(new ApiResponse(401));

            var checkpass = await this._signInManager.CheckPasswordSignInAsync(user , loginDto.Password , false);
            if(!checkpass.Succeeded)
            return Unauthorized(new ApiResponse(401));

            return new UserDto {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _token.CreateToken(user)
            };
        }
        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) {
            
         if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{"Email address is in use"}});
            }
        // gan AppUser bang du lieu nhap vao tu Client RegisterDto
        // AppUser chi tao display va name
        // nhung hien tai AppUser da la ASPNetUser KHI CHUNG TA TAO SEED UserManager<Appuser> nen no co Email va User , pass
        var user = new AppUser {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName  = registerDto.Email , 
                  
        };
        // create User , ko can kiem tra xem Email da ton tai hay chua
        // UserManager Identity  tu dong kiem tra qua result.successful
        var result =  await this._userManager.CreateAsync(user, registerDto.Password);
        
        // neu tao User khong thanh cong
        if(!result.Succeeded)
        return BadRequest(new ApiResponse(400));

        return new UserDto {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _token.CreateToken(user)
        };
        }
    }
}