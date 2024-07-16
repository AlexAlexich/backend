using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;
using GoalFinalStage.Interfaces.Repositories;
using GoalFinalStage.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinalStage.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
         _accountRepository = accountRepository;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _accountRepository.GetUserById(userId);

            if (user == null) throw new BadHttpRequestException("User not found", 404);

            await _accountRepository.DeleteUser(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _accountRepository.GetUserById(id);
        }

        public async Task UpdateUser(UpdateUserDTO updateUserDTO, string userId)
        {
            var user = await _accountRepository.GetUserById(userId);
            if (user == null) throw new BadHttpRequestException("User not found", 404);

            user.Email = updateUserDTO.Email ?? user.Email;
            user.LastName = updateUserDTO.Lastname ?? user.LastName;
            user.FirstName = updateUserDTO.Firstname ?? user.FirstName;

            if (updateUserDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUserDTO.Password));
                user.PasswordSalt = hmac.Key;

            }
            await _accountRepository.UpadateUser(user);
            await Task.CompletedTask;
        }
    }
}
