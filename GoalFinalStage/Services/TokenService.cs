using GoalFinalStage.Entities;
using GoalFinalStage.Helpers.Models;
using GoalFinalStage.Interfaces.Repositories;
using GoalFinalStage.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoalFinalStage.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }


        public string GenerateJWTToken(User user, JWTTypeModel jwtModel)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString())
            };

            var key = jwtModel==JWTTypeModel.Bearer? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AccessTokenKey"])) : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["RefreshTokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = jwtModel == JWTTypeModel.Bearer ? DateTime.UtcNow.AddMinutes(10) : DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal validateTokenAndGetPrincipal(string token, JWTTypeModel jwtModel)
        {
        
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtModel == JWTTypeModel.Bearer ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AccessTokenKey"])) : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["RefreshTokenKey"])),
                ValidateLifetime = true,
            };
            var tokenHanlder = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHanlder.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public async Task UpdateRefreshToken(string refreshToken, string userId)
        {
            await _tokenRepository.UpdateUserRefreshToken(userId, refreshToken);
            await Task.CompletedTask;
        }
    }
}
