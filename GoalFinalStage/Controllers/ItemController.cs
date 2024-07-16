using GoalFinalStage.Helpers.Models;
using GoalFinalStage.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoalFinalStage.Controllers
{
    public class ItemController: BaseApiController
    {
        private readonly IItemService _itemService;
        private readonly ITokenService _tokenService;

        public ItemController(IItemService itemService,ITokenService tokenService)
        {
            _itemService = itemService;
            _tokenService = tokenService;
        }


        [Authorize]
        [HttpPost("connectItem")]
        public async Task<ActionResult> ConnectWithItem(string itemId)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Substring("Bearer ".Length).Trim();

            _tokenService.validateTokenAndGetPrincipal(token, JWTTypeModel.Bearer);

            var userId = User.FindFirstValue("userId");
            if (userId == null) return Unauthorized();
            if (itemId == null) return BadRequest();

            await _itemService.connectWithItem(userId,itemId);
            return Ok();

        }
    }
}
