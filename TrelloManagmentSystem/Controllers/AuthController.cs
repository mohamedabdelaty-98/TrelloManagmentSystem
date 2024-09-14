using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Contracts.Authentacations;
using TrelloManagmentSystem.Contracts.Login;
using TrelloManagmentSystem.Models;
using TrelloManagmentSystem.Services;

namespace TrelloManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;


        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(model.Email, model.Password, cancellationToken);

             return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenViewModel model, CancellationToken cancellationToken)
        {
            var result = await _authService.GetRefreshTokenAsync(model.Token, model.RefreshToken, cancellationToken);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message); 
        }

    }
}
