using GoalFinalStage.DTOs.In;
using GoalFinalStage.DTOs.Out;
using GoalFinalStage.Helpers.Models;
using GoalFinalStage.Interfaces.Repositories;
using GoalFinalStage.Interfaces.Services;
using GoalFinalStage.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinalStage.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IAccountService accountService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _accountService = accountService;
        }
        public async Task<InsertResponseDTO> registerUser(RegistrationDTO registrationDTO)
        {
            var isEmailTaken = await _authRepository.IsEmailTaken(registrationDTO.Email);
            if (isEmailTaken)
                throw new BadHttpRequestException("Email already used", 400);

           var id= await _authRepository.RegisterUser(registrationDTO);
            var response = new InsertResponseDTO { Id = id };
            return response;
        }
        public async Task<TokenResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _authRepository.GetUserByEmail(loginRequestDTO.Email);
            if (user == null) throw new BadHttpRequestException("Incorrect email and password", 400);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestDTO.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new BadHttpRequestException("Incorrect email and password", 400);
            }

            var responseLoginDTO = new TokenResponseDTO
            {
                RefreshToken = _tokenService.GenerateJWTToken(user, JWTTypeModel.Refresh),
                Token = _tokenService.GenerateJWTToken(user, JWTTypeModel.Bearer),
            };

             await _tokenService.UpdateRefreshToken(responseLoginDTO.RefreshToken, user.Id.ToString());

            return responseLoginDTO;
        }

        public async Task<TokenResponseDTO> RefreshToken(string refreshToken)
        {
            var principal = _tokenService.validateTokenAndGetPrincipal(refreshToken,JWTTypeModel.Refresh);
            var userId = principal.FindFirstValue("userId");

            if (userId == null) throw new BadHttpRequestException("Invalid refresh token", 400);
            var isTokenValid = await _authRepository.HasValidRefreshRoken(refreshToken, userId);
            if (!isTokenValid) throw new BadHttpRequestException("Invalid refresh token", 400);
            var user = await _accountService.GetUserById(userId);
            if (user == null) throw new BadHttpRequestException("User not found", 404);


            var tokenResponse = new TokenResponseDTO { Token = _tokenService.GenerateJWTToken(user,JWTTypeModel.Bearer) ,RefreshToken=_tokenService.GenerateJWTToken(user,JWTTypeModel.Refresh)};
            
            await _tokenService.UpdateRefreshToken(tokenResponse.RefreshToken, user.Id.ToString());

            return tokenResponse;
        }
    }
}
