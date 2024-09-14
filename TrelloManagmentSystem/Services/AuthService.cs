using Microsoft.AspNetCore.Identity;
using SurveyBasket.ApI.Entities;
using SurveyBasket.ApI.JwtService;
using System.Security.Cryptography;
using TrelloManagmentSystem.Contracts.Login;
using TrelloManagmentSystem.Models;
using TrelloManagmentSystem.ViewModels;

namespace TrelloManagmentSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpirationDays = 14;

        public AuthService(UserManager<AppUser> userManager , 
            SignInManager<AppUser> signInManager , IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

       public async Task<ResultViewModel<LoginDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
                 if (user is null)
                return ResultViewModel<LoginDto>.Failure(ErrorCode.DoesNotExist , "User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
               if (!result.Succeeded)
                    return ResultViewModel<LoginDto>.Failure(ErrorCode.DoesNotExist, "User not found.");

            var (token, expireIn) = await _jwtProvider.CreateTokenAsync(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

            user.RefreshTokens.Add(new RefreshTokens
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new LoginDto
                (user.Id, user.FirstName, user.LastName, user.Email, token, expireIn, refreshToken, refreshTokenExpiration);
            return ResultViewModel<LoginDto>.Success(response);
        }
        public async Task<ResultViewModel<LoginDto>> GetRefreshTokenAsync(string token, string refeshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidatToken(token);
            if (userId is null)
                return ResultViewModel<LoginDto>.Failure(ErrorCode.InvalidCredentials, "Invalid Token.");
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return ResultViewModel<LoginDto>.Failure(ErrorCode.InvalidCredentials, "Invalid Token.");


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refeshToken && x.IsActive);
                 if (userRefreshToken is null)
                       return ResultViewModel<LoginDto>.Failure(ErrorCode.InvalidCredentials, "Invalid Token.");

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (newToken, newExpireIn) = await _jwtProvider.CreateTokenAsync(user);

            var newRefreshToken = GenerateRefreshToken();
            var newRefreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

            user.RefreshTokens.Add(new RefreshTokens
            {
                Token = newRefreshToken,
                ExpiresOn = newRefreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new LoginDto
                (user.Id, user.FirstName, user.LastName, user.Email, newToken, newExpireIn, newRefreshToken, newRefreshTokenExpiration);

            return ResultViewModel<LoginDto>.Success(response);

        }






        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }


    }
}
