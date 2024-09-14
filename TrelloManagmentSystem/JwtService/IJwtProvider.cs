using TrelloManagmentSystem.Models;

namespace SurveyBasket.ApI.JwtService
{
    public interface IJwtProvider
    {

        Task<(string token , DateTime expireIn )> CreateTokenAsync(AppUser user);

        string? ValidatToken(string token);

    }
}
