using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users.TestData
{
    public static class GenerateCreateUserTestData
    {
        public static CreateUserRequest GenerateUserRequest()
        {
            var addressFaker = new Faker<CreateUserAddressRequest>()
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Random.Number(1, 1000))
                .RuleFor(a => a.Zipcode, f => f.Address.ZipCode())
                .RuleFor(a => a.Geolocation, (f, a) => new CreateUserGeolocationRequest
                {
                    Lat = f.Address.Latitude().ToString(),
                    Long = f.Address.Longitude().ToString()
                });

            var faker = new Faker<CreateUserRequest>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
                .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
                .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
                .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
                .RuleFor(u => u.Address, f => {
                    var address = addressFaker.Generate();
                    return new CreateUserAddressRequest
                    {
                        City = address.City,
                        Street = address.Street,
                        Number = address.Number,
                        Zipcode = address.Zipcode,
                        Geolocation = address.Geolocation
                    };
                });


            return faker.Generate();
        }
    }
}