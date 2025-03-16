using Ambev.DeveloperEvaluation.Domain.Enums;
using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    /// <summary>
    /// API response model for the CreateUser operation.
    /// </summary>
    public class CreateUserResponse
    {
        /// <summary>
        /// The unique identifier of the created user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's full name (concatenated from first and last names).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// The user's role in the system.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// The current status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// The address details of the user.
        /// </summary>
        public CreateUserAddressResponse Address { get; set; } = new CreateUserAddressResponse();
    }

    /// <summary>
    /// API model for the address details in the CreateUserResponse.
    /// </summary>
    public class CreateUserAddressResponse
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
        /// Gets or sets the number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation details.
        /// </summary>
        public CreateUserGeolocationResponse Geolocation { get; set; } = new CreateUserGeolocationResponse();
    }

    /// <summary>
    /// API model for the geolocation details in the CreateUserAddressResponse.
    /// </summary>
    public class CreateUserGeolocationResponse
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