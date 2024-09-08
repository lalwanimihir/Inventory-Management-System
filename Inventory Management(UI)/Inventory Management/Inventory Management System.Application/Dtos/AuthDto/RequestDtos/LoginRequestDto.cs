using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Application.Dtos.AuthDto.RequestDtos
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
