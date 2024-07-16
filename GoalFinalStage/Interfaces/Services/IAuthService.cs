using GoalFinalStage.DTOs.In;
using GoalFinalStage.DTOs.Out;

namespace GoalFinalStage.Interfaces.Services
{
    public interface IAuthService
    {
        Task<InsertResponseDTO> registerUser(RegistrationDTO registrationDTO);

        Task<TokenResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<TokenResponseDTO> RefreshToken(string refreshToken);
    }
}
