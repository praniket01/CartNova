using Authentication.Dto;
using Authentication.Models;
using CartNova.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService( UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, AuthDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator) {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ResponseDto> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == loginDto.Email);

                if (user != null)
                {
                    var pwdcheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                    if (user != null && pwdcheck)
                    {

                        //Generate Token
                        var token = _jwtTokenGenerator.GenerateToken(user);

                        UserDto userDto = new()
                        {
                            Email = user.Email,
                            Id = user.Id,
                            Name = user.Name,
                            PhoneNumber = user.PhoneNumber
                        };

                        LoginResponseDto loginResponseDto = new()
                        {
                            UserDto = userDto,
                            Token = token
                        };

                        return new ResponseDto
                        {
                            IsSuccess = true,
                            Result = loginResponseDto
                        };
                    }
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid email or password"
                    };

                }
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
