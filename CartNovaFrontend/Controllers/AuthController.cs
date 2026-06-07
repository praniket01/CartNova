using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CartNovaFrontend.Models;
using CartNovaFrontend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CartNovaFrontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new() { Text = "Admin", Value = "Admin" },
                new() { Text = "Customer", Value = "Customer" }
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
           ResponseDto response = await _authService.Register(registerDto);
            ResponseDto assignRoleResponse = null;
            if(response ==null )
            {
                if(string.IsNullOrEmpty(registerDto.Role))
                {
                    registerDto.Role = "Customer";
                }
                assignRoleResponse = await _authService.AssignRole(registerDto);
                if(assignRoleResponse.Message == null)
                {
                    TempData["Success"] = "Registration successful. Please login.";
                    return RedirectToAction("Login");
                }
            
            }
            else
            {
                TempData["Error"] = response.Message;

            }
                return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ResponseDto response = await _authService.Login(loginDto);
            ResponseDto assignRoleResponse = null;
            if (response.Message == null)
            {
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                _tokenProvider.SetToken(loginResponse.Token);
                await _tokenProvider.SignInUser(loginResponse);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(loginDto);

            }
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
