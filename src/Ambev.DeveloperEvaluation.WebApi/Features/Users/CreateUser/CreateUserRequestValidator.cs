using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    /// <summary>
    /// Validator for CreateUserRequest that defines validation rules for user creation.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - FirstName: Required, length between 3 and 50 characters
    /// - LastName: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X followed by 1 to 14 digits)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// - Address: All fields must be provided and valid.
    /// </remarks>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserRequestValidator"/> class.
        /// Sets up the validation rules for user creation.
        /// </summary>
        public CreateUserRequestValidator()
        {
            // Validate the Email using a custom EmailValidator.
            RuleFor(request => request.Email)
                .SetValidator(new EmailValidator())
                .WithMessage("A valid email address is required.");

            // Ensure FirstName is not empty and has a length between 3 and 50 characters.
            RuleFor(request => request.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters long.");

            // Ensure LastName is not empty and has a length between 3 and 50 characters.
            RuleFor(request => request.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters long.");

            // Validate the Password using a custom PasswordValidator.
            RuleFor(request => request.Password)
                .SetValidator(new PasswordValidator())
                .WithMessage("Password does not meet security requirements.");

            // Validate the Phone number to match international format: starts with an optional '+' and followed by 1 to 14 digits.
            RuleFor(request => request.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in the international format (e.g., +123456789).");

            // Ensure Status is not set to an unknown value.
            RuleFor(request => request.Status)
                .NotEqual(UserStatus.Unknown)
                .WithMessage("User status cannot be Unknown.");

            // Ensure Role is not set to None.
            RuleFor(request => request.Role)
                .NotEqual(UserRole.None)
                .WithMessage("User role must be a valid role.");

            // Validate Address fields.
            // Ensure the city is provided.
            RuleFor(request => request.Address.City)
                .NotEmpty()
                .WithMessage("City is required in the address.");

            // Ensure the street is provided.
            RuleFor(request => request.Address.Street)
                .NotEmpty()
                .WithMessage("Street is required in the address.");

            // Ensure the address number is greater than zero.
            RuleFor(request => request.Address.Number)
                .GreaterThan(0)
                .WithMessage("Address number must be greater than zero.");

            // Ensure the zipcode is provided.
            RuleFor(request => request.Address.Zipcode)
                .NotEmpty()
                .WithMessage("Zipcode is required in the address.");

            // Validate nested Geolocation fields.
            // Ensure the latitude is provided.
            RuleFor(request => request.Address.Geolocation.Lat)
                .NotEmpty()
                .WithMessage("Latitude is required in the address geolocation.");

            // Ensure the longitude is provided.
            RuleFor(request => request.Address.Geolocation.Long)
                .NotEmpty()
                .WithMessage("Longitude is required in the address geolocation.");
        }
    }
}