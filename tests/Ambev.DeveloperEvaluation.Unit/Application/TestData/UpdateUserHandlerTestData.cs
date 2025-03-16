using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    /// <summary>
    /// Provides helper methods for generating test data using the Bogus library for UpdateUserCommand.
    /// This class centralizes test data generation to ensure consistency across test cases.
    /// </summary>
    public static class UpdateUserHandlerTestData
    {
        private static readonly Faker<UpdateUserCommand> updateUserCommandFaker = new Faker<UpdateUserCommand>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Address, f => new UpdateUserAddress
            {
                City = f.Address.City(),
                Street = f.Address.StreetName(),
                Number = f.Random.Number(1, 1000),
                Zipcode = f.Address.ZipCode(),
                Geolocation = new UpdateUserGeolocation
                {
                    Lat = f.Address.Latitude().ToString(),
                    Long = f.Address.Longitude().ToString()
                }
            });

        /// <summary>
        /// Generates a valid UpdateUserCommand with random but valid data.
        /// </summary>
        /// <returns>A valid UpdateUserCommand.</returns>
        public static UpdateUserCommand GenerateValidCommand()
        {
            return updateUserCommandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid UpdateUserCommand for testing validation failures.
        /// </summary>
        /// <returns>An UpdateUserCommand that should fail validation.</returns>
        public static UpdateUserCommand GenerateInvalidCommand()
        {
            return new UpdateUserCommand();
        }
    }
}