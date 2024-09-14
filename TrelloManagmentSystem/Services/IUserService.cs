using SurveyBasket.ApI.Contracts.Profile;
using TrelloManagmentSystem.ViewModels;

namespace TrelloManagmentSystem.Services
{
    public interface IUserService
    {
        Task<ResultViewModel<UserInfoDto>> GetProfileAsync(string userId);

        Task<ResultViewModel> UpdateProfileAsync(string userId, UpdateProfileViewModel request);
        Task<ResultViewModel> ChangePasswordAsync(string userId, ChangePasswordViewModel request);

    }

}
