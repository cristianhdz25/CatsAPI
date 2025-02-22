using CatAPI.BW.Interfaces.BW;
using CatAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthUserBW _authUserBW;
        
        public AuthController(IAuthUserBW authUserBW)
        {
            _authUserBW = authUserBW;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var token = await _authUserBW.LoginAsync(loginDTO.Username, loginDTO.Password);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _authUserBW.RegisterAsync(registerDTO.Username, registerDTO.Password);
            return Ok();
        }

    }
}
