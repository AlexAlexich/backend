using GoalFinalStage.Data;
using GoalFinalStage.Entities;
using GoalFinalStage.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoalFinalStage.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task connectWithItem(string userId, string itemId)
        {
            var itemUser = new ItemUser
            {
                ItemId = itemId,
                UserId = userId,
            };

            _context.ItemUsers.Add(itemUser);

            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task<Item> getItemById(string itemId)
        {
            return await _context.Items.SingleOrDefaultAsync(x => x.Id.ToString() == itemId);
        }

        public async Task<ItemUser> getItemUserByIds(string itemId, string userId)
        {
            return await _context.ItemUsers.SingleOrDefaultAsync(x => x.ItemId == itemId && x.UserId == userId);
        }
    }
}
