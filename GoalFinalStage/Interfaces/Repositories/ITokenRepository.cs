namespace GoalFinalStage.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task UpdateUserRefreshToken(string userId, string? RefreshToken);

    }
}
