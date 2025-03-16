using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the User entity that defines validation rules for user properties.
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            // Validate email: required and in valid email format.
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            // Validate Name.Firstname: required and length between 3 and 50 characters.
            RuleFor(u => u.Name.Firstname)
                .NotEmpty().WithMessage("Firstname cannot be empty.")
                .MinimumLength(3).WithMessage("Firstname must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Firstname cannot be longer than 50 characters.");

            // Validate Name.Lastname: required and length between 3 and 50 characters.
            RuleFor(u => u.Name.Lastname)
                .NotEmpty().WithMessage("Lastname cannot be empty.")
                .MinimumLength(3).WithMessage("Lastname must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Lastname cannot be longer than 50 characters.");

            // Validate password: required and at least 8 characters.
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            // Você pode complementar com um validador customizado para exigir letras maiúsculas, minúsculas, número e caractere especial.

            // Validate phone: must follow international format (using a simplified regex).
            RuleFor(u => u.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in a valid international format.");

            // Validate status: cannot be Unknown.
            RuleFor(u => u.Status)
                .NotEqual(UserStatus.Unknown)
                .WithMessage("User status cannot be Unknown.");

            // Validate role: cannot be None.
            RuleFor(u => u.Role)
                .NotEqual(UserRole.None)
                .WithMessage("User role cannot be None.");

            // Validate geolocation: Latitude must be a valid number using invariant culture.
            RuleFor(u => u.Address.Geolocation.Lat)
                .Must(lat => double.TryParse(lat, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                .WithMessage("Latitude must be a valid number.");

            // Validate geolocation: Longitude must be a valid number using invariant culture.
            RuleFor(u => u.Address.Geolocation.Long)
                .Must(lng => double.TryParse(lng, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                .WithMessage("Longitude must be a valid number.");
        }
    }
}