using CatAPI.BC.Models;
using CatAPI.BC.Utilities;
using CatAPI.BW.Interfaces.BW;
using CatAPI.BW.Interfaces.DA;

namespace CatAPI.BW.UC
{
    // This class handles business logic related to user authentication,
    // including login and registration functionalities.
    public class AuthUserBW : IAuthUserBW
    {
        private readonly IManageUserDA _manageUser;  // Interface to manage user data access
        private readonly PasswordHasher _passwordHasher;  // Utility to hash and verify passwords
        private readonly JwtGenerator _jwtGenerator;  // Utility to generate JWT tokens

        // Constructor to inject the required dependencies
        public AuthUserBW(IManageUserDA manageUser, PasswordHasher passwordHasher, JwtGenerator jwtGenerator)
        {
            _manageUser = manageUser;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        // Method to authenticate a user by validating their username and password,
        // then generating a JWT token upon successful login.
        public async Task<string> LoginAsync(string username, string password)
        {
            // Retrieve user by username
            var user = await _manageUser.GetByUsernameAsync(username);

            // Check if user exists and verify the password
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                // Throw an exception if username or password is incorrect
                throw new UnauthorizedAccessException("Username or password are incorrects");
            }

            // Generate a JWT token if authentication is successful
            return _jwtGenerator.GenerateToken(user);
        }

        // Method to register a new user with username and password.
        // If registration is successful, a success message is returned; otherwise, an error message is returned.
        public async Task<ResponseModel> RegisterAsync(string username, string password)
        {
            // Check if the username already exists in the system
            User newUser = await _manageUser.GetByUsernameAsync(username);

            if (newUser.Username != "")
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "This username already exists"
                };
            }

            // Hash the password and generate a salt
            var (hash, salt) = _passwordHasher.HashPassword(password);

            // Create a new user object with the hashed password and salt
            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                Salt = salt
            };

            // Attempt to register the user and return a response based on the result
            return await _manageUser.RegisterAsync(user) ?
                new ResponseModel
                {
                    Success = true,
                    Message = "User registered successfully"
                } :
                new ResponseModel
                {
                    Success = false,
                    Message = "Error registering user"
                };
        }
    }
}
