using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Validator for the UpdateUserCommand.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number.");

            RuleFor(u => u.Password)
                .SetValidator(new PasswordValidator());

            RuleFor(u => u.Status)
                .NotEqual(UserStatus.Unknown).WithMessage("Invalid status.");

            RuleFor(u => u.Role)
                .NotEqual(UserRole.None).WithMessage("Invalid role.");

            RuleFor(u => u.Address)
                .NotNull().WithMessage("Address is required.");

            When(u => u.Address != null, () =>
            {
                RuleFor(u => u.Address.City)
                    .NotEmpty().WithMessage("City is required.");

                RuleFor(u => u.Address.Street)
                    .NotEmpty().WithMessage("Street is required.");

                RuleFor(u => u.Address.Number)
                    .GreaterThan(0).WithMessage("Number must be greater than 0.");

                RuleFor(u => u.Address.Zipcode)
                    .NotEmpty().WithMessage("Zipcode is required.");

                RuleFor(u => u.Address.Geolocation)
                    .NotNull().WithMessage("Geolocation is required.");

                When(u => u.Address.Geolocation != null, () =>
                {
                    RuleFor(u => u.Address.Geolocation.Lat)
                        .NotEmpty().WithMessage("Latitude is required.");

                    RuleFor(u => u.Address.Geolocation.Long)
                        .NotEmpty().WithMessage("Longitude is required.");
                });
            });
        }
    }
}
