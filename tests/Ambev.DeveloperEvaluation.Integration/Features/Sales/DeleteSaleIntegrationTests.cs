using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Common.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    /// <summary>
    /// Integration tests for the SalesController's DeleteSale endpoint.
    /// </summary>
    public class DeleteSaleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public DeleteSaleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "DELETE /api/sales/{id} returns success and sale is not retrievable afterwards")]
        public async Task DeleteSale_ReturnsSuccessAndSaleIsDeleted()
        {
            // Arrange
            var createSaleRequest = GenerateCreateSaleTestData.GenerateSaleRequest();

            var createResponse = await _client.PostAsJsonAsync("/api/sales", createSaleRequest);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            createApiResponse.Should().NotBeNull();
            createApiResponse.Success.Should().BeTrue();
            createApiResponse.Data.Should().NotBeNull();
            var saleId = createApiResponse.Data.Sale.Id;
            saleId.Should().NotBeEmpty();

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/sales/{saleId}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deleteApiResponse = await deleteResponse.Content.ReadFromJsonAsync<ApiResponse>();
            deleteApiResponse.Should().NotBeNull();
            deleteApiResponse.Success.Should().BeTrue();



            // Assert
            var getResponse = await _client.GetAsync($"/api/sales/{saleId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
