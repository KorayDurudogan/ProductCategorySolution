using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductCategory.Service.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductCategory.Service.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public string CreateToken(TokenRequestDto tokenRequestDto)
        {
            if (string.IsNullOrWhiteSpace(tokenRequestDto?.Password))
                throw new ArgumentNullException();

            else if (!tokenRequestDto.Password.Equals(_configuration.GetSection(TokenConstants.TokenPassword).Value))
                throw new UnauthorizedAccessException();

            var token = new JwtSecurityToken
            (
                issuer: _configuration.GetSection(TokenConstants.TokenIssuer).Value,
                audience: _configuration.GetSection(TokenConstants.TokenAudience).Value,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection(TokenConstants.TokenSigningKey).Value)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
