using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.User.Commands;
using Project_management_system.ViewModels;
using TrelloManagmentSystem.ViewModels;

namespace TrelloManagmentSystem.Controllers
{
	public class UserController : Controller
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost("reset")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
		{
			var result = await _mediator.Send(command);
			if (result)
			{
				return Ok("Password has been reset successfully.");
			}
			return BadRequest("Invalid token or error resetting password.");
		}


		[HttpGet("VerifyEmail")]
		public async Task<IActionResult> VerifyEmail(string email, string otpCode)
		{
			var isVerified = await _mediator.Send(new VerifyOTPCommand(email, otpCode));
			if (!isVerified)
			{
				return BadRequest("email is not verified");
			}
			return Ok(ResultViewModel<bool>.Success(true, "email verified successfully"));
		}
	}


}
