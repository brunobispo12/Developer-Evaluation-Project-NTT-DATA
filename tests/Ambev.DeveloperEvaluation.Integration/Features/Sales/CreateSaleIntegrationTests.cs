using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData;

namespace Ambev.DeveloperEvaluation.IntegrationTests.Features.Sales
{
    /// <summary>
    /// Integration tests for the SalesController's CreateSale endpoint.
    /// </summary>
    public class CreateSaleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CreateSaleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Cria um HttpClient que hospeda a aplicação em memória.
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "POST /api/sales returns 201 Created for valid sale creation")]
        public async Task CreateSale_ReturnsCreated_And_CleansUp()
        {
            Guid saleId = Guid.Empty;
            try
            {
                // Arrange
                var createSaleRequest = GenerateCreateSaleTestData.GenerateSaleRequest();

                // Act
                var response = await _client.PostAsJsonAsync("/api/sales", createSaleRequest);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
                apiResponse.Should().NotBeNull();
                apiResponse.Success.Should().BeTrue();
                apiResponse.Data.Should().NotBeNull();
                apiResponse.Data.Sale.SaleNumber.Should().NotBeNullOrEmpty();
                saleId = apiResponse.Data.Sale.Id;
                saleId.Should().NotBeEmpty();
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

        [Fact(DisplayName = "POST /api/sales returns 400 Bad Request for invalid sale creation")]
        public async Task CreateSale_InvalidRequest_ReturnsBadRequest()
        {
            var invalidSaleRequest = new CreateSaleRequest
            {
                SaleDate = DateTime.UtcNow.AddDays(-1),
                Customer = "",
                Branch = "",
                IsCancelled = false,
                Items = new List<SaleItemRequest>
                {
                    new SaleItemRequest
                    {
                        Product = Guid.Empty,
                        Quantity = 0,
                        UnitPrice = 0
                    }
                }
            };

            var response = await _client.PostAsJsonAsync("/api/sales", invalidSaleRequest);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}