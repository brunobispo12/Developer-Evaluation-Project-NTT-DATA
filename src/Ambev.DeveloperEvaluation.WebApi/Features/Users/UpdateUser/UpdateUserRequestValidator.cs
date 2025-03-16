using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// Validator for UpdateUserRequest.
    /// </summary>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("User ID is required.");
            RuleFor(u => u.FirstName).NotEmpty().MaximumLength(50).WithMessage("First name is required and must be at most 50 characters.");
            RuleFor(u => u.LastName).NotEmpty().MaximumLength(50).WithMessage("Last name is required and must be at most 50 characters.");
            RuleFor(u => u.Password).SetValidator(new PasswordValidator());
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(u => u.Phone).NotEmpty().WithMessage("Phone is required.");
            RuleFor(u => u.Status).NotEqual(UserStatus.Unknown).WithMessage("Status must be valid.");
            RuleFor(u => u.Role).NotEqual(UserRole.None).WithMessage("Role must be valid.");

            RuleFor(u => u.Address).NotNull().WithMessage("Address is required.");
            RuleFor(u => u.Address.City).NotEmpty().WithMessage("City is required.");
            RuleFor(u => u.Address.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(u => u.Address.Number).GreaterThan(0).WithMessage("Number must be greater than zero.");
            RuleFor(u => u.Address.Zipcode).NotEmpty().WithMessage("Zipcode is required.");
            RuleFor(u => u.Address.Geolocation).NotNull().WithMessage("Geolocation is required.");
            RuleFor(u => u.Address.Geolocation.Lat).NotEmpty().WithMessage("Latitude is required.");
            RuleFor(u => u.Address.Geolocation.Long).NotEmpty().WithMessage("Longitude is required.");
        }
    }
}
