using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.ApI.Contracts.Profile;
using System.Security.Claims;
using TrelloManagmentSystem.Services;

namespace TrelloManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Info")]
        public async Task<ActionResult> Info()
        {
          var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.GetProfileAsync(userId);
            return Ok(result.Data);

        }


        [HttpPut("edit-profile")]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileViewModel request)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _userService.UpdateProfileAsync(userId, request);

            return NoContent();

        }


        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userService.ChangePasswordAsync(userId, request);

            return result.IsSuccess ? NoContent() : BadRequest(result.errorCode);


        }

    }
}
