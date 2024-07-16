using GoalFinalStage.Interfaces.Repositories;
using GoalFinalStage.Interfaces.Services;

namespace GoalFinalStage.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IAccountRepository _accountRepository;

        public ItemService(IItemRepository itemRepository,IAccountRepository accountRepository)
        {
           _itemRepository = itemRepository;
           _accountRepository = accountRepository;
        }
        public async Task connectWithItem(string userId, string itemId)
        {
            if (await _accountRepository.GetUserById(userId) == null) throw new BadHttpRequestException("User not found", 404);
            if(await _itemRepository.getItemById(itemId)  == null) throw new BadHttpRequestException("Item not found", 404);

           if(await _itemRepository.getItemUserByIds(itemId,userId) !=null) throw new BadHttpRequestException("Confict, you can have only one item of this type", 409);
    

           await _itemRepository.connectWithItem(userId, itemId);

            await Task.CompletedTask;
        }
    }
}
