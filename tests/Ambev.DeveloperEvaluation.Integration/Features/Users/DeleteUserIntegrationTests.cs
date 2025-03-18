using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Users.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users
{
    public class DeleteUserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DeleteUserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "DELETE /api/users/{id} returns success and user is not retrievable afterwards")]
        public async Task DeleteUser_ReturnsSuccessAndUserIsDeleted()
        {
            // Arrange
            var createUserRequest = GenerateCreateUserTestData.GenerateUserRequest();
            var createResponse = await _client.PostAsJsonAsync("/api/users", createUserRequest);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
            createApiResponse.Should().NotBeNull();
            createApiResponse.Success.Should().BeTrue();
            var userId = createApiResponse.Data.Id;
            userId.Should().NotBeEmpty();

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/users/{userId}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deleteApiResponse = await deleteResponse.Content.ReadFromJsonAsync<ApiResponse>();
            deleteApiResponse.Should().NotBeNull();
            deleteApiResponse.Success.Should().BeTrue();

            // Assert
            var getResponse = await _client.GetAsync($"/api/users/{userId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}