using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Dtos.RegisterUserDto;
using hr.makemystamp.com.core.Entities;
using hr.makemystamp.com.core.EventHandler;
using hr.makemystamp.com.core.Events;
using MediatR;

namespace hr.makemystamp.com.application.Segrigations.Commands.UserRegister
{
    public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<ResponseDto>;

    public class RegisterCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks, IRepository<User> userRepository, IRepository<Identity> identityRepository, IRepository<RolesUser> rolesUserRepository, IRepository<Role> roleRepository, IPasswordService passwordService) : IRequestHandler<RegisterUserCommand, ResponseDto>
    {
        public readonly IMediator _mediator = mediator;
        public readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        public readonly IRepository<User> _userRepository = userRepository;
        public readonly IRepository<Identity> _identityRepository = identityRepository;
        public readonly IRepository<RolesUser> _rolesUserRepository = rolesUserRepository;
        public readonly IPasswordService _passwordService = passwordService;
        public readonly IRepository<Role> _roleRepository = roleRepository;

        public async Task<ResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                var errorResponse = new ResponseDto(string.Empty, "Invalid request");
                await _mediator.Publish(new ResponseEvent(errorResponse), cancellationToken);
                return errorResponse;
            }

            try
            {
                var existingRole = _userRepository.GetAll().FirstOrDefault(a => a.Email == request.RegisterUserDto.Email);
                if (existingRole != null)
                {
                    var errorResponse = new ResponseDto(existingRole.Email, $"{existingRole.Email} is already registerd, Please login with this email");
                    await _mediator.Publish(new ResponseEvent(errorResponse), cancellationToken);
                    return errorResponse;
                }

                var User = new User
                {
                    Email = request.RegisterUserDto.Email,
                    FirstName = request.RegisterUserDto.FirstName,
                    LastName = request.RegisterUserDto.LastName,
                    PhoneNumber = request.RegisterUserDto.PhoneNumber,
                    External_Id = Guid.NewGuid(),
                };
                long id = 1;
                var role = _roleRepository.GetByIdAsync(id);
                var roleUsers = new RolesUser
                {
                    RoleId = role.Result.RoleId,
                    UserId = User.Id
                };

                var (Password, Salt, HashedPassword) = _passwordService.HashPassword(request.RegisterUserDto.Password);
                var identity = new Identity
                {
                    HashedPassword = HashedPassword,
                    Salt = Salt,
                    UserId = User.Id
                };
                _userRepository.Insert(User);
                _rolesUserRepository.Insert(roleUsers);
                _identityRepository.Insert(identity);
                await _unitOfWorks.SaveChangesAsync(cancellationToken); // Await for async operation
                await _mediator.Publish(new UserCreationEmailHandler(User.Id, User.FirstName, User.Email, "Admin"), cancellationToken);
                var successResponse = new ResponseDto(User.Email, "User registration successfully");
                await _mediator.Publish(new ResponseEvent(successResponse), cancellationToken);

                return successResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new ResponseDto(string.Empty, ex.Message);
                await _mediator.Publish(new ResponseEvent(errorResponse), cancellationToken);
                return errorResponse;
            }
        }
    }
}
