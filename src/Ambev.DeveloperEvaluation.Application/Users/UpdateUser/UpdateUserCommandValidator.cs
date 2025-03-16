using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Validator for <see cref="UpdateUserCommand"/> that defines validation rules for updating a user.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("User Id is required for update.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");

            RuleFor(u => u.Status)
                .NotEqual(UserStatus.Unknown).WithMessage("Status must be specified.");

            RuleFor(u => u.Role)
                .NotEqual(UserRole.None).WithMessage("Role must be specified.");

            RuleFor(u => u.Address).NotNull().WithMessage("Address is required.");
            RuleFor(u => u.Address.City).NotEmpty().WithMessage("City is required.");
            RuleFor(u => u.Address.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(u => u.Address.Number).GreaterThan(0).WithMessage("Number must be greater than 0.");
            RuleFor(u => u.Address.Zipcode).NotEmpty().WithMessage("Zipcode is required.");
            RuleFor(u => u.Address.Geolocation).NotNull().WithMessage("Geolocation is required.");
            RuleFor(u => u.Address.Geolocation.Lat).NotEmpty().WithMessage("Latitude is required.");
            RuleFor(u => u.Address.Geolocation.Long).NotEmpty().WithMessage("Longitude is required.");
        }
    }
}