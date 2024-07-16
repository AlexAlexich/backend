using GoalFinalStage.Data;
using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;
using GoalFinalStage.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinalStage.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task UpadateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
