using MediatR;
 using Microsoft.EntityFrameworkCore;
using TrelloManagmentSystem.Exceptions;
using TrelloManagmentSystem.Repositories.GenericRepositories;
 
namespace TrelloManagmentSystem.CQRS.User.Queries
{
    public record GetUserByEmailQuery(string email):IRequest<TrelloManagmentSystem.Models.User>
    {
    }
    public record GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, TrelloManagmentSystem.Models.User>
    {
        private IGenericRepositories<TrelloManagmentSystem.Models.User> _userRepository;
        public GetUserByEmailHandler(IGenericRepositories<TrelloManagmentSystem.Models.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<TrelloManagmentSystem.Models.User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user=await _userRepository.GetAll().FirstOrDefaultAsync(u=>u.Email==request.email);
            if (user==null)
            {
                throw new BusinessException(ErrorCode.UserEmailNotFound,"Can not find user with this email");
            }
            return user;
        }
    }
}
