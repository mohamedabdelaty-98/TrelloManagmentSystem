using FluentValidation;

namespace TrelloManagmentSystem.Contracts.Login
{
    public class LoginViewModelValidators : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidators()
        {
            RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
