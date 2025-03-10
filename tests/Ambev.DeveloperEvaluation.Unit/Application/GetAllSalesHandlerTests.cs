using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for <see cref="GetAllSalesHandler"/>.
    /// </summary>
    public class GetAllSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetAllSalesHandler _handler;

        public GetAllSalesHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllSalesHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid GetAllSalesCommand, when Handle is invoked, then it returns a success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = GetAllSalesHandlerTestData.GenerateValidCommand();
            var saleOne = new Sale("Sale001", DateTime.Now.AddDays(-1), "Customer A", "Branch A") { Id = Guid.NewGuid() };
            var saleTwo = new Sale("Sale002", DateTime.Now.AddDays(-2), "Customer B", "Branch B") { Id = Guid.NewGuid() };

            var salesList = new List<Sale> { saleOne, saleTwo };
            var paginatedSales = new PaginatedList<Sale>(
                items: salesList,
                count: 10,
                pageNumber: command.PageNumber,
                pageSize: command.PageSize
            );

            _saleRepository.GetAllAsync(command.PageNumber, command.PageSize, command.Order, Arg.Any<CancellationToken>())
                .Returns(paginatedSales);

            var saleDtoOne = new SaleDto { Id = saleOne.Id, SaleNumber = saleOne.SaleNumber };
            var saleDtoTwo = new SaleDto { Id = saleTwo.Id, SaleNumber = saleTwo.SaleNumber };
            _mapper.Map<List<SaleDto>>(paginatedSales).Returns(new List<SaleDto> { saleDtoOne, saleDtoTwo });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Sales.Should().HaveCount(2);
            result.TotalCount.Should().Be(10);
            result.TotalPages.Should().Be((int)Math.Ceiling(10 / (double)command.PageSize));
            result.PageNumber.Should().Be(command.PageNumber);

            await _saleRepository.Received(1).GetAllAsync(
                command.PageNumber,
                command.PageSize,
                command.Order,
                Arg.Any<CancellationToken>()
            );

            _mapper.Received(1).Map<List<SaleDto>>(paginatedSales);
        }

        [Fact(DisplayName = "Given an invalid GetAllSalesCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = GetAllSalesHandlerTestData.GenerateInvalidCommand();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given a valid command with ordering, when Handle is invoked, it uses the correct ordering in the repository call")]
        public async Task Handle_ValidRequest_AppliesCorrectOrdering()
        {
            // Arrange
            var command = GetAllSalesHandlerTestData.GenerateValidCommand();
            command.Order = "SaleDate desc, SaleNumber asc";

            var paginatedSales = new PaginatedList<Sale>(new List<Sale>(), 0, command.PageNumber, command.PageSize);
            _saleRepository.GetAllAsync(command.PageNumber, command.PageSize, command.Order, Arg.Any<CancellationToken>())
                .Returns(paginatedSales);

            _mapper.Map<List<SaleDto>>(paginatedSales).Returns(new List<SaleDto>());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Sales.Should().BeEmpty();
            await _saleRepository.Received(1).GetAllAsync(
                command.PageNumber,
                command.PageSize,
                command.Order,
                Arg.Any<CancellationToken>()
            );
        }
    }
}