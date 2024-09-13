using FluentValidation;

namespace SurveyBasket.ApI.Contracts.Profile
{
    public class ChangePasswordViewModelValidators: AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidators()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();


            RuleFor(x => x.NewPassword)
                    .NotEmpty()
                    .NotEqual(x => x.CurrentPassword)
                    .WithMessage("New Password Cant Equal Current Password");
        }

    }
}
