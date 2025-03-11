using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Common.DTO;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateSaleHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid CreateSaleCommand, when Handle is invoked, then it returns a success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            var sale = new Sale(
                command.SaleNumber,
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = Guid.NewGuid()
            };

            // Create a SaleDto to be returned in the result
            var saleDto = new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
                Items = new System.Collections.Generic.List<SaleItemDto>()
            };

            // Create the result wrapping the full SaleDto
            var createSaleResult = new CreateSaleResult
            {
                Sale = saleDto
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(createSaleResult);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                          .Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Sale.Should().NotBeNull();
            result.Sale.Id.Should().Be(sale.Id);
            result.Sale.SaleNumber.Should().Be(sale.SaleNumber);

            await _saleRepository.Received(1).CreateAsync(
                Arg.Is<Sale>(s => s.SaleNumber == command.SaleNumber),
                Arg.Any<CancellationToken>()
            );
        }

        [Fact(DisplayName = "Given an invalid CreateSaleCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateSaleCommand();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given an existing SaleNumber, when Handle is invoked, then it throws InvalidOperationException")]
        public async Task Handle_SaleNumberAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            var existingSale = new Sale(
                command.SaleNumber,
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = Guid.NewGuid()
            };

            _saleRepository.GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>())
                           .Returns(existingSale);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with number {command.SaleNumber} already exists");
        }

        [Fact(DisplayName = "Given a valid command, when Handle is invoked, then it maps command to Sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            var sale = new Sale(
                command.SaleNumber,
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = Guid.NewGuid()
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>())
                           .Returns(sale);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
                c.SaleNumber == command.SaleNumber &&
                c.SaleDate == command.SaleDate
            ));
        }
    }
}