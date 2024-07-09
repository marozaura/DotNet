using System.IdentityModel.Tokens.Jwt;
using AspTestProject.BLL.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using AspTestProject.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspTestProject.BLL.Services.Implementations;

public class TokenGeneratorService : ITokenGeneratorService
{
    private IOptions<JwtSettings> _settings;

    public TokenGeneratorService(IOptions<JwtSettings> settings)
    {
        _settings = settings;
    }

    public string GenerateJwt(long userId, string userEmail)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.Value.JwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = CreateClaimsIdentity(userId, userEmail),
            Expires = DateTime.UtcNow.Date.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity CreateClaimsIdentity(long userId, string userEmail)
    {
        var claims = new List<Claim>
        {
            new("UserId", userId.ToString()),
            new("UserEmail", userEmail)
        };

        return new ClaimsIdentity(claims);
    }
}