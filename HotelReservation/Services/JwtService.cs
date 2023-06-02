using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelReservation.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
//     public string GenerateToken(Usuario usuario)
//     {
//     var tokenHandler = new JwtSecurityTokenHandler();
//     var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
//     var claims = usuario.GetClaims();
//     var tokenDescriptor = new SecurityTokenDescriptor
//     {
//         Subject = new ClaimsIdentity(claims),
//         Expires = DateTime.UtcNow.AddHours(8),
//         SigningCredentials = new SigningCredentials(
//             new SymmetricSecurityKey(key),
//             SecurityAlgorithms.HmacSha256Signature)
//     };
//     var token = tokenHandler.CreateToken(tokenDescriptor);
//     return tokenHandler.WriteToken(token);
//     }
// public ClaimsPrincipal DecodeJwtToken(string token)
// {
//     var tokenHandler = new JwtSecurityTokenHandler();
//     var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

//     var tokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(key),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         TryAllIssuerSigningKeys = true,
//         IssuerSigningKeys = new List<SecurityKey>() { new SymmetricSecurityKey(key) }
//     };

//     var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
//     return principal;
// }




    public string GenerateJwtToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Email, usuario.Correo)
        }),
            Expires = DateTime.UtcNow.AddHours(1) // Ajusta la duración del token según sea necesario
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
//     public ClaimsPrincipal DecodeJwtToken(string token)
//     {
//         Console.WriteLine(token);
//         try
//     {
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
//             Console.WriteLine("primer linea");
//         var validationParameters = new TokenValidationParameters
// {
//     ValidateIssuerSigningKey = true,
//     ValidateIssuer = false,
//     ValidateAudience = false,
//     TryAllIssuerSigningKeys = true,
//     IssuerSigningKeys = new List<SecurityKey>() { new SymmetricSecurityKey(key) }
// };
//             Console.WriteLine("Validacion linea");
//         var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
//             Console.WriteLine(principal);
//         return principal;
//     }
//     catch (Exception ex)
//     {
//         // Si ocurre algún error durante la desencriptación, puedes manejarlo aquí
//         Console.WriteLine($"Error al desencriptar el token: {ex.Message}");
//         return null;
//     }
}
