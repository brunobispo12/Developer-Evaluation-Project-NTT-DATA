using System;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        /// <summary>
        /// The unique identifier of the user to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The user's current status.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// The user's role.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// The user's address details.
        /// </summary>
        public UpdateUserAddress Address { get; set; } = new UpdateUserAddress();
    }

    /// <summary>
    /// Represents the address information for the user.
    /// </summary>
    public class UpdateUserAddress
    {
        /// <summary>
        /// The city where the user resides.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// The street name.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// The street number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The postal code.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// The geolocation information for the address.
        /// </summary>
        public UpdateUserGeolocation Geolocation { get; set; } = new UpdateUserGeolocation();
    }

    /// <summary>
    /// Represents the geolocation (latitude and longitude) for the address.
    /// </summary>
    public class UpdateUserGeolocation
    {
        /// <summary>
        /// The latitude value.
        /// </summary>
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// The longitude value.
        /// </summary>
        public string Long { get; set; } = string.Empty;
    }
}