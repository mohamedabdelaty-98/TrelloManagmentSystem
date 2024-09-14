using TrelloManagmentSystem.Contracts.Login;
using TrelloManagmentSystem.ViewModels;

namespace TrelloManagmentSystem.Services
{
    public interface IAuthService
    {
        Task<ResultViewModel<LoginDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

        Task<ResultViewModel<LoginDto>> GetRefreshTokenAsync(string token, string refeshToken, CancellationToken cancellationToken = default);

    }
}
