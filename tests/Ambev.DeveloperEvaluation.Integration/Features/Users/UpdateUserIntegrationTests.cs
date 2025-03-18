using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Users.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users
{
    /// <summary>
    /// Integration tests for the UsersController's UpdateUser endpoint.
    /// </summary>
    public class UpdateUserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UpdateUserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "PUT /api/users/update returns updated user details for a valid update request")]
        public async Task UpdateUser_ReturnsUpdatedUserDetails_And_CleansUpData()
        {
            Guid userId = Guid.Empty;
            try
            {
                // Arrange
                var createUserRequest = GenerateCreateUserTestData.GenerateUserRequest();
                var createResponse = await _client.PostAsJsonAsync("/api/users", createUserRequest);
                createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
                var createApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
                createApiResponse.Should().NotBeNull();
                createApiResponse.Success.Should().BeTrue();
                createApiResponse.Data.Should().NotBeNull();
                userId = createApiResponse.Data.Id;
                userId.Should().NotBeEmpty();

                // Arrange
                var updateUserRequest = new UpdateUserRequest
                {
                    Id = userId,
                    FirstName = "UpdatedFirstName",
                    LastName = "UpdatedLastName",
                    Email = "updated.email@example.com",
                    Phone = "+5511999999999",
                    Password = "NewP@ssw0rd!",
                    Status = createUserRequest.Status,
                    Role = createUserRequest.Role,
                    Address = new UpdateAddressRequest
                    {
                        City = "Updated City",
                        Street = "Updated Street",
                        Number = 123,
                        Zipcode = "11111-111",
                        Geolocation = new UpdateGeolocationRequest
                        {
                            Lat = "10.1234",
                            Long = "-10.1234"
                        }
                    }
                };

                // Act
                var updateResponse = await _client.PutAsJsonAsync("/api/users/update", updateUserRequest);
                updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var updateApiResponse = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateUserResponse>>();
                updateApiResponse.Should().NotBeNull();
                updateApiResponse.Success.Should().BeTrue();
                updateApiResponse.Data.Should().NotBeNull();
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
    }
}