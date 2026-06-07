using CartNovaFrontend.Models;

namespace CartNovaFrontend.Service
{
    public interface ITokenProvider
    {
        public string? GetToken();
        public void SetToken(string token);

        public void clearToken();

        public Task SignInUser(LoginResponseDto loginResponseDto);
    }
}
