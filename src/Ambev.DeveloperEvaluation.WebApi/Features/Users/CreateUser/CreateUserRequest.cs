using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    /// <summary>
    /// Represents a request to create a new user in the system.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the initial status of the user account.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public CreateUserAddressRequest Address { get; set; } = new CreateUserAddressRequest();
    }

    /// <summary>
    /// Represents the address information for creating a user.
    /// </summary>
    public class CreateUserAddressRequest
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the house or building number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation details.
        /// </summary>
        public CreateUserGeolocationRequest Geolocation { get; set; } = new CreateUserGeolocationRequest();
    }

    /// <summary>
    /// Represents the geolocation information for a user's address.
    /// </summary>
    public class CreateUserGeolocationRequest
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public string Long { get; set; } = string.Empty;
    }
}