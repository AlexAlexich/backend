using GoalFinalStage.Data;
using GoalFinalStage.DTOs.In;
using GoalFinalStage.Entities;
using GoalFinalStage.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinalStage.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
           _context = context;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email) ;
        }

        public async Task<bool> HasValidRefreshRoken(string refreshToken, string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id.ToString() == userId);
            if (user == null) return false;
            if (user.RefreshToken != refreshToken) return false;
            //if (user.RefreshTokenExpiryTime < DateTime.UtcNow) return false;


            return true;
        }

        public async Task<string> RegisterUser(RegistrationDTO registrationDTO)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = registrationDTO.Email,
                FirstName = registrationDTO.Firstname,
                LastName = registrationDTO.Lastname,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationDTO.Password)),
                PasswordSalt = hmac.Key,

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var id = _context.Users.SingleOrDefault(x => x.Email == user.Email).Id.ToString();

            return id;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
