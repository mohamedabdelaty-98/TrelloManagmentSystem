using Microsoft.AspNetCore.Identity;
using TrelloManagmentSystem.DTO;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem.Services
{
	public class AccountService
	{

		private readonly UserManager<ApplicationUser> _userManager;

		public AccountService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
		{
			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
			if (user == null)
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });

			var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.ResetCode, resetPasswordDto.NewPassword);

			return result;
		}

		public async Task<string> GeneratePasswordResetTokenAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return null;

			return await _userManager.GeneratePasswordResetTokenAsync(user);
		}
	}

}

