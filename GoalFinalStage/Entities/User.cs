using System.ComponentModel.DataAnnotations;

namespace GoalFinalStage.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public List<Item> Items { get; set; } = new List<Item>() { };

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
