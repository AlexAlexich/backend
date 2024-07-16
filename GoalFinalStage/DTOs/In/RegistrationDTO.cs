using System.ComponentModel.DataAnnotations;

namespace GoalFinalStage.DTOs.In
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Firstname is required")]

        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]

        public string Lastname { get; set; }
    }
}
