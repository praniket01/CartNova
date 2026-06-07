using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Authentication.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Authentication.Service.Impl
{
    public class JwtTokenGenerateor : IJwtTokenGenerator
    {
        private readonly IConfiguration configuration;
        public JwtTokenGenerateor(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(ApplicationUser applicationuser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = System.Text.Encoding.UTF8.GetBytes(configuration["ApiSettings:JwtSettings:Key"]);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, applicationuser.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, applicationuser.Id),
                new Claim(JwtRegisteredClaimNames.Email, applicationuser.Email)
            };


            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configuration["ApiSettings:JwtSettings:Audience"],
                Issuer = configuration["ApiSettings:JwtSettings:Issuer"],
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
