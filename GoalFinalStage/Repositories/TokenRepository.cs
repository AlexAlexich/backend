using GoalFinalStage.Data;
using GoalFinalStage.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoalFinalStage.Repositories
{
    public class TokenRepository: ITokenRepository
    {
        private readonly DataContext _context;

        public TokenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task UpdateUserRefreshToken(string userId, string? RefreshToken)
        {
            var userToUpdate = await _context.Users.SingleOrDefaultAsync(x => x.Id.ToString() == userId);
            if (userToUpdate == null) return;
            userToUpdate.RefreshToken = RefreshToken;

            userToUpdate.RefreshTokenExpiryTime = RefreshToken != null ? DateTime.UtcNow.AddDays(7) : null;
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
