using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;

namespace GoalFinalStage.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task DeleteUser(User  user);
        Task UpadateUser(User user);

        Task<User> GetUserById(string id);
    }
}
