using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// Validator for CreateUserCommand that defines validation rules for user creation command.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - FirstName: Required, must be between 3 and 50 characters
    /// - LastName: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X followed by 1 to 14 digits)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// - Address: All fields must be provided and valid
    /// </remarks>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
        /// </summary>
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.Email)
                .SetValidator(new EmailValidator());

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(user => user.LastName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(user => user.Password)
                .SetValidator(new PasswordValidator());

            RuleFor(user => user.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$");

            RuleFor(user => user.Status)
                .NotEqual(UserStatus.Unknown);

            RuleFor(user => user.Role)
                .NotEqual(UserRole.None);

            // Address validation
            RuleFor(user => user.Address).NotNull().WithMessage("Address must be provided.");
            When(user => user.Address != null, () =>
            {
                RuleFor(user => user.Address.City)
                    .NotEmpty().WithMessage("City is required.");

                RuleFor(user => user.Address.Street)
                    .NotEmpty().WithMessage("Street is required.");

                RuleFor(user => user.Address.Number)
                    .GreaterThan(0).WithMessage("Number must be greater than 0.");

                RuleFor(user => user.Address.Zipcode)
                    .NotEmpty().WithMessage("Zipcode is required.");

                RuleFor(user => user.Address.Geolocation).NotNull().WithMessage("Geolocation is required.");
                When(user => user.Address.Geolocation != null, () =>
                {
                    RuleFor(user => user.Address.Geolocation.Lat)
                        .NotEmpty().WithMessage("Latitude is required.");

                    RuleFor(user => user.Address.Geolocation.Long)
                        .NotEmpty().WithMessage("Longitude is required.");
                });
            });
        }
    }
}