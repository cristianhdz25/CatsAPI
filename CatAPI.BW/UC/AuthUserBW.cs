using CatAPI.BC.Models;
using CatAPI.BC.Utilities;
using CatAPI.BW.Interfaces.BW;
using CatAPI.BW.Interfaces.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatAPI.BW.UC
{
    public class AuthUserBW : IAuthUserBW
    {
        private readonly IManageUserDA _manageUser;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtGenerator _jwtGenerator;

        public AuthUserBW(IManageUserDA manageUser, PasswordHasher passwordHasher, JwtGenerator jwtGenerator)
        {
            _manageUser = manageUser;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _manageUser.GetByUsernameAsync(username);
            if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");
            }

            return _jwtGenerator.GenerateToken(user);
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var (hash, salt) = _passwordHasher.HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = hash,
                Salt = salt
            };

            return await _manageUser.RegisterAsync(user);
        }
    }
}
