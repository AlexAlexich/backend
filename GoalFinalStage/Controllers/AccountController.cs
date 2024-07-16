using GoalFinalStage.DTOs.In;
using GoalFinalStage.Helpers.Models;
using GoalFinalStage.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoalFinalStage.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
           _accountService = accountService;
           _tokenService = tokenService;
        }
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteUser()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Substring("Bearer ".Length).Trim();
            var principal = _tokenService.validateTokenAndGetPrincipal(token, JWTTypeModel.Bearer);

            var userId = principal.FindFirstValue("userId");
            if(userId == null) return Unauthorized();

            await _accountService.DeleteUser(userId);
            return Ok();
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Substring("Bearer ".Length).Trim();
           var principal = _tokenService.validateTokenAndGetPrincipal(token, JWTTypeModel.Bearer);

            var userId = principal.FindFirstValue("userID");

            if (userId == null) return Unauthorized();

            if (updateUserDTO == null) return BadRequest("Invalid request");

            await _accountService.UpdateUser(updateUserDTO, userId);

            return Ok();
        }


      
    }
}
