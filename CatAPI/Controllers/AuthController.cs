using CatAPI.BW.Interfaces.BW;
using CatAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    // The ApiController attribute automatically validates request data and binds incoming data to action parameters
    [ApiController]
    // Define the base route for the controller, here it's mapped to "api/auth"
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // Private readonly field to access business logic layer methods for user authentication
        private readonly IAuthUserBW _authUserBW;

        // Constructor to inject the IAuthUserBW service into the controller
        public AuthController(IAuthUserBW authUserBW)
        {
            _authUserBW = authUserBW;
        }

        // POST: api/auth/login
        // Action method to handle user login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // Call the business layer to handle the login process and get the token
            var token = await _authUserBW.LoginAsync(loginDTO.Username ?? "", loginDTO.Password ?? "");

            // Return the generated token in the response
            return Ok(new { Token = token });
        }

        // POST: api/auth/register
        // Action method to handle user registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // Call the business layer to register the user
            var response = await _authUserBW.RegisterAsync(registerDTO.Username, registerDTO.Password);

            // Return the response from the registration process
            return Ok(response);
        }
    }
}
