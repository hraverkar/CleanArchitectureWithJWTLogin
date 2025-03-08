using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Dtos;
using hr.makemystamp.com.core.Dtos.LoginDto;
using hr.makemystamp.com.core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hr.makemystamp.com.application.Segrigations.Commands.Login.Command
{
    public record LoginCommand(LoginDto LoginDto) : IRequest<TokenResponse>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Identity> _identityRepository;
        private readonly IOptions<JWTSettings> _jwtSettings; // Inject using IOptions
        private readonly IPasswordService _passwordService;
        private readonly IRepository<RolesUser> _roleUserRepository;
        private readonly IRepository<Role> _roleRepository;

        public LoginCommandHandler(
            IRepository<User> userRepository,
            IRepository<Identity> identityRepository,
            IOptions<JWTSettings> jwtSettings,  // Inject using IOptions
            IPasswordService passwordService, IRepository<RolesUser> roleUserRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _identityRepository = identityRepository;
            _jwtSettings = jwtSettings;
            _passwordService = passwordService;
            _roleUserRepository = roleUserRepository;
            _roleRepository = roleRepository;
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Email == request.LoginDto.Email, cancellationToken);

            if (user == null)
            {
                return new TokenResponse { ErrorMessage = "Invalid email or password." };
            }
            var roleUser = _roleUserRepository.GetAll().FirstOrDefault(r => r.UserId == user.Id);
            var role = _roleRepository.GetAll().FirstOrDefault(u => u.RoleId == roleUser.RoleId);

            var identity = _identityRepository.GetAll().FirstOrDefault(p=> p.UserId == user.Id);

            if (identity == null || !_passwordService.VerifyPassword(request.LoginDto.Password, identity.Salt, identity.HashedPassword))
            {
                return new TokenResponse { ErrorMessage = "Invalid email or password." };
            }

            var token = GenerateJwtToken(user.FirstName, user.Email);

            return new TokenResponse
            {
                AccessToken = token,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.ExpiryInMinutes),
                User = user,
                Role = role
            };
        }

        private string GenerateJwtToken(string firstName, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.ExpiryInMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, firstName),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expiryDate).ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Value.Issuer,
                _jwtSettings.Value.Audience,
                claims,
                expires: expiryDate,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
