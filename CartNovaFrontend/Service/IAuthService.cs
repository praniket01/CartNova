using CartNovaFrontend.Models;

namespace CartNovaFrontend.Service
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegisterDto registerDto);
        Task<ResponseDto> Login(LoginDto loginDto);
        Task<ResponseDto> AssignRole(RegisterDto registerDto);

    }
}
