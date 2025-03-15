using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Integration.Features.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    /// <summary>
    /// Integration tests for the SalesController's GetAllSales endpoint.
    /// </summary>
    public class GetAllSalesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetAllSalesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "GET /api/sales/all returns a paginated list of sales and cleans up test data")]
        public async Task GetAllSales_ReturnsPaginatedSales_And_CleansUpData()
        {
            List<Guid> createdSaleIds = new List<Guid>();

            try
            {
                // Arrange
                int numberOfSales = 2;

                for (int i = 0; i < numberOfSales; i++)
                {
                    var saleRequest = GenerateCreateSaleTestData.GenerateSaleRequest();
                    var createResponse = await _client.PostAsJsonAsync("/api/sales", saleRequest);
                    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

                    var apiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
                    apiResponse.Should().NotBeNull();
                    apiResponse.Success.Should().BeTrue();
                    apiResponse.Data.Should().NotBeNull();

                    createdSaleIds.Add(apiResponse.Data.Sale.Id);
                }

                // Act
                var getAllResponse = await _client.GetAsync("/api/sales/all?pageNumber=1&pageSize=100");
                getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                var getAllApiResponse = await getAllResponse.Content.ReadFromJsonAsync<ApiResponseWithData<GetAllSalesResponse>>();
                getAllApiResponse.Should().NotBeNull();
                getAllApiResponse.Success.Should().BeTrue();
                getAllApiResponse.Data.Should().NotBeNull();
                getAllApiResponse.Data.Sales.Should().NotBeEmpty();

                // Assert
                foreach (var saleId in createdSaleIds)
                {
                    getAllApiResponse.Data.Sales.Should().Contain(s => s.Id == saleId);
                }
            }
            finally
            {
                foreach (var saleId in createdSaleIds)
                {
                    await _client.DeleteAsync($"/api/sales/{saleId}");
                }
            }
        }
    }
}