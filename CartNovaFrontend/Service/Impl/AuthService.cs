using CartNovaFrontend.Models;
using CartNovaFrontend.Utility;

namespace CartNovaFrontend.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService _baseService)
        {
            this._baseService = _baseService;
        }
        public async Task<ResponseDto> AssignRole(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = registerDto,
                Url = SD.AuthAPIBase + "/api/auth/assignrole",
            });

        }

        public async Task<ResponseDto> Login(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = loginDto,
                Url = SD.AuthAPIBase + "/api/auth/login",
            });
        }

        public async Task<ResponseDto> Register(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = registerDto,
                Url = SD.AuthAPIBase + "/api/auth/register",
            });
        }
    }
}
