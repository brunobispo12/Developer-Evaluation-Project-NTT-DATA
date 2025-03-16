using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a user in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class User : BaseEntity, IUser
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password for authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's role in the system.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the user's current status.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last update to the user's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user's name information.
        /// Contains first and last name.
        /// </summary>
        public Name Name { get; set; } = new Name();

        /// <summary>
        /// Gets or sets the user's address information.
        /// </summary>
        public Address Address { get; set; } = new Address();

        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        /// <returns>The user's ID as a string.</returns>
        string IUser.Id => Id.ToString();

        /// <summary>
        /// Gets the user's role in the system.
        /// </summary>
        /// <returns>The user's role as a string.</returns>
        string IUser.Role => Role.ToString();

        /// <summary>
        /// Gets the user's FirstName in the system.
        /// </summary>
        /// <returns>The user's first name as a string.</returns>
        string IUser.FirstName => Name.Firstname;

        /// <summary>
        /// Gets the user's LastName in the system.
        /// </summary>
        /// <returns>The user's last name as a string.</returns>
        string IUser.LastName => Name.Lastname;

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Performs validation of the user entity using the UserValidator rules.
        /// </summary>
        /// <returns>A <see cref="ValidationResultDetail"/> with validation results.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new UserValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Activates the user account.
        /// </summary>
        public void Activate()
        {
            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Deactivates the user account.
        /// </summary>
        public void Deactivate()
        {
            Status = UserStatus.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Blocks the user account.
        /// </summary>
        public void Suspend()
        {
            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}