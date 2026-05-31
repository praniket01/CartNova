using Authentication.Models;

namespace Authentication.Service
{
    public interface IJwtTokenGenerator
    {
       public string GenerateToken(ApplicationUser applicationuser);
    }
}
