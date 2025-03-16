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
    /// </remarks>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
        /// </summary>
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.Email)
                .SetValidator(new EmailValidator());

            RuleFor(request => request.FirstName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(request => request.LastName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(request => request.Password)
                .SetValidator(new PasswordValidator());

            RuleFor(request => request.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$");

            RuleFor(request => request.Status)
                .NotEqual(UserStatus.Unknown);

            RuleFor(request => request.Role)
                .NotEqual(UserRole.None);
        }
    }
}