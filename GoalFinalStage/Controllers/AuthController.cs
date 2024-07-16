using GoalFinalStage.DTOs.In;
using GoalFinalStage.DTOs.Out;
using GoalFinalStage.Interfaces.Services;
using GoalFinalStage.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalFinalStage.Controllers
{
    public class AuthController:BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<InsertResponseDTO>> Registration(RegistrationDTO registrationDTO)
        {
            if (registrationDTO == null) return BadRequest();

            var inserResponse =  await _authService.registerUser(registrationDTO);

            return Ok(inserResponse);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null) return BadRequest();
           
            var loginResponse =await _authService.Login(loginRequestDTO);
            return Ok(loginResponse);
        }


        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponseDTO>> refreshToken(string refreshToken)
        {
            if (refreshToken == null) return BadRequest("Invalid request");


            var tokenDto = await _authService.RefreshToken(refreshToken);

            return Ok(tokenDto);

        }
    }
}
