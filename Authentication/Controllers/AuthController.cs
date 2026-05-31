using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Service;
using Authentication.Service.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ResponseDto _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if(string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            _response.IsSuccess = false;
            _response.Message = result;
            return BadRequest(_response);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            return Ok("Login");
        }
    }
}
