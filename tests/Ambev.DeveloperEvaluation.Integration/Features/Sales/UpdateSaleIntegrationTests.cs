using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using SaleItemRequest = Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale.SaleItemRequest;
using Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    /// <summary>
    /// Integration tests for the SalesController's UpdateSale endpoint.
    /// </summary>
    public class UpdateSaleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UpdateSaleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "PUT /api/sales/update returns updated sale details for a valid update request")]
        public async Task UpdateSale_ReturnsUpdatedSaleDetails_And_CleansUpData()
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

                // Arrange
                var updateSaleRequest = new UpdateSaleRequest
                {
                    Id = saleId,
                    SaleDate = createSaleRequest.SaleDate,
                    Customer = Guid.NewGuid(),
                    Branch = Guid.NewGuid(),
                    IsCancelled = true,
                    Items = new List<SaleItemRequest>
                    {
                        new SaleItemRequest
                        {
                            Product = Guid.NewGuid(),
                            Quantity = 10,
                            UnitPrice = 18.00m
                        }
                    }
                };

                // Act
                var updateResponse = await _client.PutAsJsonAsync("/api/sales/update", updateSaleRequest);
                updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var updateApiResponse = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
                updateApiResponse.Should().NotBeNull();
                updateApiResponse.Success.Should().BeTrue();
                updateApiResponse.Data.Should().NotBeNull();

                // Assert
                updateApiResponse.Data.Sale.Id.Should().Be(saleId);
                updateApiResponse.Data.Sale.IsCancelled.Should().BeTrue();
                updateApiResponse.Data.Sale.SaleDate.Should().BeCloseTo(updateSaleRequest.SaleDate, TimeSpan.FromSeconds(1));
            }
            finally
            {
                if (saleId != Guid.Empty)
                {
                    await _client.DeleteAsync($"/api/sales/{saleId}");
                }
            }
        }
    }
}