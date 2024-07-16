using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;

namespace GoalFinalStage.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<string> RegisterUser(RegistrationDTO registrationDTO);

        Task<User> GetUserByEmail(string email);

        Task<bool> HasValidRefreshRoken(string refreshToken, string userId);
        Task<bool> IsEmailTaken(string email);


    }
}
