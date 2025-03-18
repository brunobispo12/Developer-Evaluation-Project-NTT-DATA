using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.Integration.Features.Users.TestData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users
{
    public class CreateUserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CreateUserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "POST /api/users returns 201 Created for valid user creation")]
        public async Task CreateUser_ReturnsCreated_And_CleansUp()
        {
            Guid userId = Guid.Empty;
            try
            {
                // Arrange
                var createUserRequest = GenerateCreateUserTestData.GenerateUserRequest();

                // Act
                var response = await _client.PostAsJsonAsync("/api/users", createUserRequest);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
                apiResponse.Should().NotBeNull();
                apiResponse.Success.Should().BeTrue();
                apiResponse.Data.Should().NotBeNull();
                userId = apiResponse.Data.Id;
                userId.Should().NotBeEmpty();
            }
            finally
            {
                // Cleanup
                if (userId != Guid.Empty)
                {
                    await _client.DeleteAsync($"/api/users/{userId}");
                }
            }
        }

        [Fact(DisplayName = "POST /api/users returns 400 Bad Request for invalid user creation")]
        public async Task CreateUser_InvalidRequest_ReturnsBadRequest()
        {
            var invalidRequest = new CreateUserRequest
            {
                FirstName = "",
                LastName = "",
                Email = "invalid-email",
                Password = "short",
                Phone = "",
                Address = new CreateUserAddressRequest
                {
                    City = "",
                    Street = "",
                    Number = 0,
                    Zipcode = "",
                    Geolocation = new CreateUserGeolocationRequest
                    {
                        Lat = "",
                        Long = ""
                    }
                }
            };

            var response = await _client.PostAsJsonAsync("/api/users", invalidRequest);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
