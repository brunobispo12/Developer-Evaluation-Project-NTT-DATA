using Ambev.DeveloperEvaluation.Domain.Enums;
using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// Represents the response for an updated user.
    /// </summary>
    public class UpdateUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
        public UpdateAddressResponse Address { get; set; } = new UpdateAddressResponse();
    }

    public class UpdateAddressResponse
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Zipcode { get; set; } = string.Empty;
        public UpdateGeolocationResponse Geolocation { get; set; } = new UpdateGeolocationResponse();
    }

    public class UpdateGeolocationResponse
    {
        public string Lat { get; set; } = string.Empty;
        public string Long { get; set; } = string.Empty;
    }
}