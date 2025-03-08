using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Dtos.RoleDto;
using hr.makemystamp.com.core.Entities;
using hr.makemystamp.com.core.Events;
using MediatR;

namespace hr.makemystamp.com.application.Segrigations.Commands.Roles
{
    public record RoleCommand(RoleDto RoleDto) : IRequest<ResponseDto>;

    public class RoleCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks, IRepository<Role> roleRepository) : IRequestHandler<RoleCommand, ResponseDto>
    {
        public readonly IMediator _mediator = mediator;
        public readonly IUnitOfWorks _unitOfWorks = unitOfWorks;
        public readonly IRepository<Role> _roleRepository = roleRepository;

        public async Task<ResponseDto> Handle(RoleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                var errorResponse = new ResponseDto(string.Empty, "Invalid request");
                await _mediator.Publish(new ResponseEvent(errorResponse), cancellationToken);
                return errorResponse;
            }

            try
            {
                var existingRole = _roleRepository.GetAll().FirstOrDefault(a => a.Name == request.RoleDto.Name);
                if (existingRole != null)
                {
                    var errorResponse = new ResponseDto(string.Empty, "Role already exists");
                    await _mediator.Publish(new ResponseEvent(errorResponse), cancellationToken);
                    return errorResponse;
                }

                var newRole = new Role
                {
                    Name = request.RoleDto.Name,
                    Description = request.RoleDto.Description,
                    CreatedAt = DateTime.UtcNow, // Use UTC time for consistency
                };

                _roleRepository.Insert(newRole);
                await _unitOfWorks.SaveChangesAsync(cancellationToken); // Await for async operation

                var successResponse = new ResponseDto(newRole.RoleId.ToString(), "Role details saved successfully");
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
