using GoalFinalStage.Entities;
using GoalFinalStage.Helpers.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GoalFinalStage.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJWTToken(User user,JWTTypeModel typeModel);

        ClaimsPrincipal validateTokenAndGetPrincipal(string token, JWTTypeModel jwtModel);
        Task UpdateRefreshToken(string refreshToken, string userId);



    }
}
