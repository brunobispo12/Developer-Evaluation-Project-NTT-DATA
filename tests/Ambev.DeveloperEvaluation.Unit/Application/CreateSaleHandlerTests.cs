﻿using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ISaleNumberGenerator _saleNumberGenerator;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _saleNumberGenerator = Substitute.For<ISaleNumberGenerator>();
            _handler = new CreateSaleHandler(_saleRepository, _mapper, _saleNumberGenerator);
        }

        [Fact(DisplayName = "Given a valid CreateSaleCommand, when Handle is invoked, then it returns a success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Simulate generator returning a SaleNumber based on sale date.
            string generatedSaleNumber = $"AMV-{command.SaleDate:yyyyMMdd}-000001";
            _saleNumberGenerator.GenerateSaleNumberAsync(command.SaleDate, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(generatedSaleNumber));

            // Create a Sale entity with the generated SaleNumber.
            var sale = new Sale(
                generatedSaleNumber,
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
                Items = new List<SaleItemDto>()
            };

            // Create the result wrapping the full SaleDto
            var createSaleResult = new CreateSaleResult
            {
                Sale = saleDto
            };

            // Setup AutoMapper and repository mappings
            _mapper.Map<Sale>(Arg.Any<CreateSaleCommand>()).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(createSaleResult);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Sale.Should().NotBeNull();
            result.Sale.Id.Should().Be(sale.Id);
            // Verifica se o SaleNumber foi gerado corretamente
            result.Sale.SaleNumber.Should().Be(generatedSaleNumber);

            // Verifica que o método do repositório foi chamado com a entidade com o SaleNumber gerado.
            await _saleRepository.Received(1).CreateAsync(
                Arg.Is<Sale>(s => s.SaleNumber == generatedSaleNumber),
                Arg.Any<CancellationToken>()
            );
        }

        [Fact(DisplayName = "Given an invalid CreateSaleCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange: Cria um comando vazio para disparar a validação.
            var command = new CreateSaleCommand();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given an existing generated SaleNumber, when Handle is invoked, then it throws InvalidOperationException")]
        public async Task Handle_SaleNumberAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            string generatedSaleNumber = $"AMV-{command.SaleDate:yyyyMMdd}-000001";
            _saleNumberGenerator.GenerateSaleNumberAsync(command.SaleDate, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(generatedSaleNumber));

            var existingSale = new Sale(
                generatedSaleNumber,
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = Guid.NewGuid()
            };

            _saleRepository.GetBySaleNumberAsync(generatedSaleNumber, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Sale?>(existingSale));

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with number {generatedSaleNumber} already exists");
        }

        [Fact(DisplayName = "Given a valid command, when Handle is invoked, then it maps command to Sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            string generatedSaleNumber = $"AMV-{command.SaleDate:yyyyMMdd}-000001";
            _saleNumberGenerator.GenerateSaleNumberAsync(command.SaleDate, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(generatedSaleNumber));

            var sale = new Sale(
                generatedSaleNumber,
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = Guid.NewGuid()
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sale));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
                // Asserção: o comando já deve ter o SaleNumber gerado
                c.SaleDate == command.SaleDate &&
                c.Customer == command.Customer &&
                c.Branch == command.Branch &&
                c.SaleNumber == generatedSaleNumber
            ));
        }
    }
}