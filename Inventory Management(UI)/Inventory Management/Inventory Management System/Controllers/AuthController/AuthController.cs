using Inventory_Management_System.Application.Dtos.AuthDto.RequestDtos;
using Inventory_Management_System.Application.Services.IAuthService;
using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new ApplicationUser
            {
                UserName = registerRequestDto.Name,
                Email = registerRequestDto.Email,
                PhoneNumber = registerRequestDto.PhoneNumber,
            };
            string[] role = ["User"];
            var identityResult = await _authRepository.RegisterAsync(identityUser, registerRequestDto.Password, role);

            if (identityResult != null)
            {
                return Ok(identityResult);
            }
            return BadRequest(identityResult);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _authRepository.LoginAsync(loginRequest.Email, loginRequest.Password);
            if (!user.IsLoggedIn)
            {
                return Ok(user);
            }
            return Ok(user);
        }
    }
}
