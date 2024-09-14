using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrelloManagmentSystem.CQRS.User.Commands;
using TrelloManagmentSystem.DTO;
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
		[HttpPost("resetPassword")]
		public async Task<IActionResult> ResetPassword([FromBody] TrelloManagmentSystem.CQRS.User.Commands.ResetPasswordCommand command)
		{
			var result = await _mediator.Send(command);
			if (result)
			{
				return Ok("Password successfully.");
			}
			return BadRequest("Invalid token");
		}

		[HttpGet("VerifyEmail")]
		public async Task<IActionResult> VerifyEmail([FromBody] TrelloManagmentSystem.CQRS.User.Commands.VerifyOTPCommand ResetPasswordDto)
		{
			var isVerified = await _mediator.Send(ResetPasswordDto);
			if (!isVerified)
			{
				return BadRequest("email is not verified");
			}
			return Ok(ResultViewModel<bool>.Success(true, "email verified successfully"));
		}


		 

	}


}
