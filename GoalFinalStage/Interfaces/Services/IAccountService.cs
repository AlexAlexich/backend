using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;

namespace GoalFinalStage.Interfaces.Services
{
    public interface IAccountService
    {

        Task<User> GetUserById(string id);

        Task DeleteUser(string userId);

        Task UpdateUser(UpdateUserDTO updateUserDTO, string userId);
    }
}
