using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Dtos.RoleDto;
using hr.makemystamp.com.core.Entities;
using MediatR;

namespace hr.makemystamp.com.application.Segrigations.Queries.Roles
{
    public record GetRoleByIdQuery(long Id) : IRequest<RoleResponseDto>;

    public class GetRoleByIdQueryHandler(IRepository<Role> roleRepository) : IRequestHandler<GetRoleByIdQuery, RoleResponseDto>
    {
        private readonly IRepository<Role> _roleRepository = roleRepository;

        public async Task<RoleResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var singleRole = _roleRepository.GetByIdAsync(request.Id);
            var roleResponse = new RoleResponseDto
            {
                RoleId = singleRole.Result.RoleId,
                Description = singleRole.Result.Description,
                Name = singleRole.Result.Name,
                CreatedAt = singleRole.Result.CreatedAt,
                UpdatedAt = singleRole.Result.UpdatedAt
            };
            return roleResponse;
        }
    }
}

