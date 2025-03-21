﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    /// <summary>
    /// Command for creating a new user.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a user, 
    /// including first name, last name, password, phone number, email, status, role, and address.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateUserResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateUserCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateUserCommand : IRequest<CreateUserResult>
    {
        /// <summary>
        /// Gets or sets the first name of the user to be created.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user to be created.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public CreateUserAddress Address { get; set; } = new CreateUserAddress();

        /// <summary>
        /// Validates the command using CreateUserCommandValidator.
        /// </summary>
        /// <returns>A ValidationResultDetail containing the validation results.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CreateUserCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }

    /// <summary>
    /// Represents the address information for creating a user.
    /// </summary>
    public class CreateUserAddress
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
        public CreateUserGeolocation Geolocation { get; set; } = new CreateUserGeolocation();
    }

    /// <summary>
    /// Represents the geolocation information for a user's address.
    /// </summary>
    public class CreateUserGeolocation
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