using GoalFinalStage.Entities;

namespace GoalFinalStage.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task connectWithItem(string userId, string itemId);

        Task<Item> getItemById(string itemId);
        Task<ItemUser> getItemUserByIds(string itemId, string userId);
    }
}
