using System;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// Represents the response returned after successfully creating a new user.
    /// </summary>
    public class CreateUserResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the created user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the created user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the created user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the created user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role of the created user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the current status of the created user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the address details of the created user.
        /// </summary>
        public CreateUserAddressResult Address { get; set; } = new CreateUserAddressResult();
    }

    /// <summary>
    /// Represents the address details in the CreateUserResult.
    /// </summary>
    public class CreateUserAddressResult
    {
        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of the address.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode of the address.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation details of the address.
        /// </summary>
        public CreateUserGeolocationResult Geolocation { get; set; } = new CreateUserGeolocationResult();
    }

    /// <summary>
    /// Represents the geolocation details in the CreateUserAddressResult.
    /// </summary>
    public class CreateUserGeolocationResult
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