﻿using MediatR;
using Project_management_system.CQRS.User.Queries;
using TrelloManagmentSystem.Exceptions;
using TrelloManagmentSystem.Repositories.GenericRepositories;

namespace Project_management_system.CQRS.User.Commands
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
            var user =await _mediator.Send(new GetUserByEmailQuery(request.email));
            if (user == null)
            {
                throw new BusinessException(ErrorCode.UserEmailNotFound,"Can not find user with that email");
            }
            if (user.OtpExpiry==null||user.Otp!=request.otpCode|| user.OtpExpiry < DateTime.UtcNow)
            {
                throw new BusinessException(ErrorCode.InvalidOTP, "Invalid otp");
            }
            user.IsVerified = true;
            user.Otp = null;
            user.OtpExpiry = null;
            _userRepository.SaveChanges();
            return true;
        }
    }
}
