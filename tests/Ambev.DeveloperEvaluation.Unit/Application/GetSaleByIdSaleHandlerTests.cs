using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for <see cref="GetSaleByIdHandler"/>.
    /// </summary>
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleByIdHandler _handler;

        public GetSaleByIdHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleByIdHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid GetSaleByIdCommand, when Handle is invoked, then it returns a successful result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = new GetSaleByIdCommand { Id = Guid.NewGuid() };

            var sale = new Sale("Sale001", DateTime.Now.AddDays(-1), "Customer A", "Branch A")
            {
                Id = command.Id
            };

            var saleDto = new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
                Items = new List<SaleItemDto>()
            };

            var expectedResult = new GetSaleByIdResult { Sale = saleDto };

            // Mock the repository and mapper
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(sale);

            _mapper.Map<GetSaleByIdResult>(sale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Sale.Should().NotBeNull();
            result.Sale!.Id.Should().Be(command.Id);
            result.Sale.SaleNumber.Should().Be(sale.SaleNumber);

            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<GetSaleByIdResult>(sale);
        }

        [Fact(DisplayName = "Given an invalid GetSaleByIdCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange: Use an empty Guid to simulate invalid input
            var command = new GetSaleByIdCommand { Id = Guid.Empty };

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given a valid command with non-existent sale, when Handle is invoked, then it throws KeyNotFoundException")]
        public async Task Handle_NonExistentSale_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new GetSaleByIdCommand { Id = Guid.NewGuid() };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Sale?)null);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} was not found.");
        }
    }
}