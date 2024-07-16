using System.ComponentModel.DataAnnotations;

namespace GoalFinalStage.DTOs.In
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
