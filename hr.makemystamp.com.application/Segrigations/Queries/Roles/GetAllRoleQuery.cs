using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Dtos.RoleDto;
using hr.makemystamp.com.core.Entities;
using MediatR;

namespace hr.makemystamp.com.application.Segrigations.Queries.Roles
{
    public record GetAllRoleQuery() : IRequest<RoleResponse>;

    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, RoleResponse>
    {
        private readonly IRepository<Role> _roleRepository;
        public GetAllRoleQueryHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<RoleResponse> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var allRoles = _roleRepository.GetAll().ToList();
            var userList = allRoles.Select(r =>
            {
                return new RoleResponseDto
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                };
            }).ToList();

            var userInfo = new RoleResponse
            {
                count = userList.Count,
                items = userList
            };
            return userInfo;
        }
    }
}
