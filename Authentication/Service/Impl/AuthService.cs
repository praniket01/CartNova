using Authentication.Dto;
using Authentication.Models;
using CartNova.Services.Data;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        public AuthService( UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, AuthDbContext dbContext) {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task<ResponseDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                NormalizedEmail = registerDto.Email.ToUpper(),
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _dbContext.ApplicationUsers.First(u => u.UserName == registerDto.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    Id = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
    }
}
