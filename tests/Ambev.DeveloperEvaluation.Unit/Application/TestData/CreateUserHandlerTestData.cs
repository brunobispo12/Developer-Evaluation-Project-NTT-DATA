using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateUserHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid CreateUserCommand entities.
        /// The generated commands will have valid:
        /// - FirstName and LastName (using internet names)
        /// - Password (meeting complexity requirements)
        /// - Email (valid format)
        /// - Phone (Brazilian format)
        /// - Status (Active or Suspended)
        /// - Role (Customer or Admin)
        /// - Address (all fields provided with valid values)
        /// </summary>
        private static readonly Faker<CreateUserCommand> createUserHandlerFaker = new Faker<CreateUserCommand>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
            .RuleFor(u => u.Address, f => new CreateUserAddress
            {
                City = f.Address.City(),
                Street = f.Address.StreetName(),
                Number = f.Random.Number(1, 1000),
                Zipcode = f.Address.ZipCode(),
                Geolocation = new CreateUserGeolocation
                {
                    Lat = f.Address.Latitude().ToString(),
                    Long = f.Address.Longitude().ToString()
                }
            });

        /// <summary>
        /// Generates a valid CreateUserCommand with randomized data.
        /// The generated command will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid CreateUserCommand with randomly generated data.</returns>
        public static CreateUserCommand GenerateValidCommand()
        {
            return createUserHandlerFaker.Generate();
        }
    }
}