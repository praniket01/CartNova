using Authentication.Dto;

namespace Authentication.Service
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto registerDto);
        Task<ResponseDto> Login(LoginDto loginDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
