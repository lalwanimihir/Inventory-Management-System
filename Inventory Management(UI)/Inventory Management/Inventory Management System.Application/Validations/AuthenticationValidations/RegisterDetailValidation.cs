using FluentValidation;
using Inventory_Management_System.Application.Dtos.AuthDto.RequestDtos;
using System.Text.RegularExpressions;

namespace Inventory_Management_System.Application.Validations.AuthenticationValidations
{
    public class RegisterDetailValidation : AbstractValidator<RegisterRequestDto>
    {
        public RegisterDetailValidation()
        {
            RuleFor(x => x.Name)
                        .NotNull().NotEmpty()
                        .WithMessage("Name is required.")
                        .MaximumLength(30)
                        .WithMessage("Name contains maximum 30 characters.");
            RuleFor(x => x.Email)
                         .NotEmpty()
                         .NotNull()
                         .WithMessage("Email is required.")
                         .EmailAddress().MaximumLength(100)
                         .WithMessage("Email should be a valid email address and should contain maximum 100 characters");
            RuleFor(x => x.Password)
                         .NotEmpty()
                         .NotNull()
                         .WithMessage("Password is required.")
                         .Must(IsValidPassword)
                         .WithMessage("Password should contain min 6, max 20, should contain one upper case, one lower case, one special symbol, and one digit");

            RuleFor(x => x.PhoneNumber)
                         .NotNull().NotEmpty()
                         .WithMessage("Phone Number is required")
                         .MinimumLength(10)
                         .MaximumLength(10)
                         .Must(IsValidPhone)
                         .WithMessage("Phone number contains minimum and maximum 10 digits.");
        }

        private static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, "\\A[0-9]{10}\\z");
        }
        private static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,20}$");
        }
    }
}
