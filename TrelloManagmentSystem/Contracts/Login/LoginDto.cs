namespace TrelloManagmentSystem.Contracts.Login
{
    public record LoginDto
    (
            string Id,
            string FirstName,
            string LastName,
            string? Email,
            string Token,
            DateTime ExpireIn,
            string RefreshToken,
            DateTime RefreshTokenExpiration
     );
}
