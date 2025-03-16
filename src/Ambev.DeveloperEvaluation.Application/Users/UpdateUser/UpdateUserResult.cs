using System;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Represents the result of the update user operation.
    /// </summary>
    public class UpdateUserResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated user's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated user's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated user's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated user's role.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the updated user's status.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the updated user's address.
        /// </summary>
        public UpdateUserAddressResult Address { get; set; } = new UpdateUserAddressResult();
    }

    /// <summary>
    /// Represents the updated address details of a user.
    /// </summary>
    public class UpdateUserAddressResult
    {
        /// <summary>
        /// Gets or sets the city of the user's address.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street of the user's address.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the house number of the user's address.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode of the user's address.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation of the user's address.
        /// </summary>
        public UpdateUserGeolocationResult Geolocation { get; set; } = new UpdateUserGeolocationResult();
    }

    /// <summary>
    /// Represents the geolocation details for an address.
    /// </summary>
    public class UpdateUserGeolocationResult
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