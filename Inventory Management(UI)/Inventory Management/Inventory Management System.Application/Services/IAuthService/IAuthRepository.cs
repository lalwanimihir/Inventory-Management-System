using Inventory_Management_System.Application.Dtos.AuthDto.ResponseDtos;
using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using Inventory_Management_System.Shared.Response;

namespace Inventory_Management_System.Application.Services.IAuthService
{
    public interface IAuthRepository
    {
        Task<ResponseModal> RegisterAsync(ApplicationUser identityUser, string password, string[] role);
        Task<LoginResponseDto> LoginAsync(string email, string password);
    }
}
