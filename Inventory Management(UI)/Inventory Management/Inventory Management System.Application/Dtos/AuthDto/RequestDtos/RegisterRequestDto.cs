using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Application.Dtos.AuthDto.RequestDtos
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
