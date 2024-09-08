using Inventory_Management_System.Application.Dtos.AuthDto.ResponseDtos;
using Inventory_Management_System.Application.Services.IAuthService;
using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using Inventory_Management_System.Shared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Infrastructure.AuthService
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthRepository(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<ResponseModal> RegisterAsync(ApplicationUser identityUser, string password, string[] role)
        {
            var response = new ResponseModal();
            ApplicationUser? user = await _userManager.FindByEmailAsync(identityUser.Email);

            if (user == null)
            {
                var identityResult = await _userManager.CreateAsync(identityUser, password);

                if (identityResult.Succeeded)
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, role);
                    if (identityResult.Succeeded)
                    {
                        response.IsSucceeded = true;
                        response.DescriptionMessage = Registered;
                        response.Data = identityResult;
                        return response;
                    }
                }
                else
                {
                    response.IsSucceeded = false;
                    response.DescriptionMessage = AlreadyExist;
                    response.Data = identityResult;
                    return response;
                }
            }
            response.IsSucceeded = false;
            response.DescriptionMessage = EmailExist;
            return response;
        }
        private string CreateJWTToken(ApplicationUser user, List<string> roles)
        {
            List<Claim> claims = new()
            {
                   new (ClaimTypes.Email, user.Email ?? ""),
                   new (ClaimTypes.Name, user.UserName ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponseDto> LoginAsync(string email, string password)
        {
            LoginResponseDto response = new();
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        string jwtToken = CreateJWTToken(user, roles.ToList());

                        response.AccessToken = jwtToken;
                        response.IsLoggedIn = true;
                        response.Role = [.. roles];
                        response.UserId = user.Id;
                        response.UserName = user.UserName;
                        response.Message = LoggedIn;
                        return response;
                    }
                }
                else
                {
                    response.IsLoggedIn = false;
                    response.Message = InvalidPassword;
                    return response;
                }
            }
            response.IsLoggedIn = false;
            response.Message = InvalidEmail;
            return response;
        }
    }
}
