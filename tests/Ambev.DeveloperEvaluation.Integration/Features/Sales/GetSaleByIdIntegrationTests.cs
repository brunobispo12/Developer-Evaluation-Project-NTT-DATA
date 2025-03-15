using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    /// <summary>
    /// Integration tests for the SalesController's GetSaleById endpoint.
    /// </summary>
    public class GetSaleByIdIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetSaleByIdIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/sales/{id} returns sale details for a valid sale id")]
        public async Task GetSaleById_ReturnsSaleDetails_And_CleansUpData()
        {
            Guid saleId = Guid.Empty;
            try
            {
                // Arrange
                var createSaleRequest = GenerateCreateSaleTestData.GenerateSaleRequest();

                var createResponse = await _client.PostAsJsonAsync("/api/sales", createSaleRequest);
                createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
                var createApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
                createApiResponse.Should().NotBeNull();
                createApiResponse.Success.Should().BeTrue();
                createApiResponse.Data.Should().NotBeNull();
                saleId = createApiResponse.Data.Sale.Id;
                saleId.Should().NotBeEmpty();

                // Act
                var getResponse = await _client.GetAsync($"/api/sales/{saleId}");
                getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var getApiResponse = await getResponse.Content.ReadFromJsonAsync<ApiResponseWithData<GetSaleByIdResponse>>();
                getApiResponse.Should().NotBeNull();
                getApiResponse.Success.Should().BeTrue();
                getApiResponse.Data.Should().NotBeNull();
            }
            finally
            {
                // Cleanup
                if (saleId != Guid.Empty)
                {
                    await _client.DeleteAsync($"/api/sales/{saleId}");
                }
            }
        }

        [Fact(DisplayName = "GET /api/sales/{id} returns 404 Not Found for non-existing sale")]
        public async Task GetSaleById_NonExistingSale_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/sales/{nonExistingId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}