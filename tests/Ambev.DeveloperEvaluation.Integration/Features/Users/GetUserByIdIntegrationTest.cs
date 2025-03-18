using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Users.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users
{
    public class GetUserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetUserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/users/{id} returns user details for a valid user id")]
        public async Task GetUser_ReturnsUserDetails_And_CleansUpData()
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
                userId = createApiResponse.Data.Id;
                userId.Should().NotBeEmpty();

                // Act
                var getResponse = await _client.GetAsync($"/api/users/{userId}");
                getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var getApiResponse = await getResponse.Content.ReadFromJsonAsync<ApiResponseWithData<GetUserResponse>>();
                getApiResponse.Should().NotBeNull();
                getApiResponse.Success.Should().BeTrue();
                getApiResponse.Data.Should().NotBeNull();
            }
            finally
            {
                if (userId != Guid.Empty)
                {
                    await _client.DeleteAsync($"/api/users/{userId}");
                }
            }
        }

        [Fact(DisplayName = "GET /api/users/{id} returns 404 Not Found for non-existing user")]
        public async Task GetUser_NonExistingUser_ReturnsNotFound()
        {
            var nonExistingId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/users/{nonExistingId}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}