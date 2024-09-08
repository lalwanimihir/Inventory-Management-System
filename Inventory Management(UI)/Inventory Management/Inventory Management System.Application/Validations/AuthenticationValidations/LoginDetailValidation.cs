using FluentValidation;
using Inventory_Management_System.Application.Dtos.AuthDto.RequestDtos;
using System.Text.RegularExpressions;

namespace Inventory_Management_System.Application.Validations.AuthenticationValidations
{
    public class LoginDetailValidation:AbstractValidator<LoginRequestDto>
    {
        public LoginDetailValidation() { 
        
             RuleFor(x=>x.Email).NotEmpty()
                .NotNull()
                .WithMessage("Email is required!")
                .EmailAddress()
                .WithMessage("Email should be a valid email address");
            RuleFor(x=>x.Password).NotEmpty()
                .NotNull()
                .WithMessage("Password is required!")
                .Must(IsValidPassword)
                .WithMessage("Password should contain min 6, max 20, should contain one upper case, one lower case, one special symbol, and one digit");
        }
        private static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,20}$");
        }
    }
}
