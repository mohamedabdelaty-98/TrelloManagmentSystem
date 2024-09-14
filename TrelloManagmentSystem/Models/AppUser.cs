using Microsoft.AspNetCore.Identity;
using SurveyBasket.ApI.Entities;

namespace TrelloManagmentSystem.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<RefreshTokens> RefreshTokens { get; set; } = new List<RefreshTokens>();

    }
}
