using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SurveyBasket.ApI.Contracts.Profile;
using TrelloManagmentSystem.Models;
using TrelloManagmentSystem.ViewModels;
using TrelloManagmentSystem.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TrelloManagmentSystem.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultViewModel<UserInfoDto>> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId)
                                                                    .Select(x => new UserInfoDto(
                                                                            x.FirstName,
                                                                            x.LastName,
                                                                            x.Email,
                                                                            x.UserName
                                                                        )).SingleOrDefaultAsync();




            return ResultViewModel<UserInfoDto>.Success(user);
        }

        public async Task<ResultViewModel> UpdateProfileAsync(string userId, UpdateProfileViewModel request)
        {
            var user = await _userManager.Users
                                                        .Where(x => x.Id == userId)
                                                        .ExecuteUpdateAsync(setters =>
                                                            setters
                                                                    .SetProperty(x => x.FirstName, request.FirstName)
                                                                    .SetProperty(x => x.LastName, request.LastName)
                                                           );

            return ResultViewModel.Success();
        }


        public async Task<ResultViewModel> ChangePasswordAsync(string userId, ChangePasswordViewModel request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return ResultViewModel.Success();

            var error = result.Errors.First();
            return ResultViewModel.Failure(ErrorCode.PasswordChangeFailed);

        }



    }
}
