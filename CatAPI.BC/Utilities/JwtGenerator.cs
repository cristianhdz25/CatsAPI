using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CatAPI.BC.Models;
using Microsoft.IdentityModel.Tokens;

public class JwtGenerator
{
    private readonly string _secretKey;
    private readonly int _expiryInMinutes;

    public JwtGenerator(string secretKey, int expiryInMinutes)
    {
        _secretKey = secretKey;
        _expiryInMinutes = expiryInMinutes;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expiryInMinutes), // Tiempo de expiración
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature) // Algoritmo de firma
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}