using FluentValidation;

namespace SurveyBasket.ApI.Contracts.Profile
{
    public class UpdateProfileViewModelValidators : AbstractValidator<UpdateProfileViewModel>
    {
        public UpdateProfileViewModelValidators()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();


            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
