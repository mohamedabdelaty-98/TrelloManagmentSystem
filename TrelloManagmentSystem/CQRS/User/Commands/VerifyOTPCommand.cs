using MediatR;
using TrelloManagmentSystem.CQRS.User.Queries;
using TrelloManagmentSystem.Exceptions;
using TrelloManagmentSystem.Repositories.GenericRepositories;

namespace TrelloManagmentSystem.CQRS.User.Commands
{
	public record VerifyOTPCommand(string email,string otpCode):IRequest<bool>
    {
    }
    public record VerifyOTPHandler : IRequestHandler<VerifyOTPCommand, bool>
    {
        private IMediator _mediator;
        private IGenericRepositories<TrelloManagmentSystem.Models.User> _userRepository;
        
        public VerifyOTPHandler(IMediator mediator, IGenericRepositories<TrelloManagmentSystem.Models.User> userRepository)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }


		public async Task<bool> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
		{
 			var user = await _mediator.Send(new GetUserByEmailQuery(request.email));

 			if (user == null)
			{
 				return false;
			}

 			if (!IsOtpValid(user, request.otpCode))
			{
 				return false;
			}

 			user.IsVerified = true;
			user.Otp = null;   
			user.OtpExpiry = null;

			_userRepository.Update(user);
			_userRepository.SaveChanges();

 			return true;
		}

 		private bool IsOtpValid(TrelloManagmentSystem.Models.User user, string otpCode)
		{
 			return user.Otp == otpCode && user.OtpExpiry.HasValue && user.OtpExpiry.Value >= DateTime.UtcNow;
		}



		 
	}
}
