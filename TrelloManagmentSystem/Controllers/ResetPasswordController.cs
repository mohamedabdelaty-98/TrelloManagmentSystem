using Microsoft.AspNetCore.Mvc;
using TrelloManagmentSystem.DTO;
using TrelloManagmentSystem.Services;

namespace TrelloManagmentSystem.Controllers
{
	public class ResetPasswordController : Controller
	{
		private readonly AccountService _accountService;

		//public UserController(IMediator mediator)
		//{
		//	_mediator = mediator;
		//}
		
		[HttpPost("forgot-password")]
		public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordDto forgotPasswordDto)
		{
			var token = await _accountService.GeneratePasswordResetTokenAsync(forgotPasswordDto.Email);
			if (token == null)
				return NotFound("User not found");

			// هنا ترسل الرمز للمستخدم عبر البريد الإلكتروني
			return Ok(new { ResetToken = token });
		}


		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
		{
			var result = await _accountService.ResetPasswordAsync(resetPasswordDto);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok("Password reset successfully");
		}

	}
}
