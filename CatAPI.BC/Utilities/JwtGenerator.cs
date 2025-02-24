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

    // Constructor to initialize the secret key and expiry time for the JWT token
    public JwtGenerator(string secretKey, int expiryInMinutes)
    {
        _secretKey = secretKey;
        _expiryInMinutes = expiryInMinutes;
    }

    // Method to generate a JWT token for a given user
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // Manage JWT tokens
        var key = Encoding.ASCII.GetBytes(_secretKey); // Convert the secret key to bytes

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
        }; // Token descriptor

        var token = tokenHandler.CreateToken(tokenDescriptor); // Create the token
        return tokenHandler.WriteToken(token); // Return the token as a string
    }
}