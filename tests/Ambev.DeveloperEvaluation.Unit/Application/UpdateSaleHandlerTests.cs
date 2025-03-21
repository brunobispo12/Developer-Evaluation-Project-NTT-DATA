﻿using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.DTO;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Unit tests for <see cref="UpdateSaleHandler"/>.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid UpdateSaleCommand, when Handle is invoked, then it returns a success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            var existingSale = new Sale(
                "OriginalNumber",
                DateTime.Now.AddDays(-2),
                Guid.NewGuid(),
                Guid.NewGuid()
            )
            {
                Id = command.Id
            };

            var updatedSale = new Sale(
                "OriginalNumber",
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = command.Id
            };

            var saleDto = new SaleDto
            {
                Id = updatedSale.Id,
                SaleNumber = updatedSale.SaleNumber,
                SaleDate = updatedSale.SaleDate,
                Customer = updatedSale.Customer,
                Branch = updatedSale.Branch,
                IsCancelled = updatedSale.IsCancelled,
                TotalAmount = updatedSale.TotalAmount,
                Items = new System.Collections.Generic.List<SaleItemDto>()
            };

            var updateSaleResult = new UpdateSaleResult
            {
                Sale = saleDto
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingSale);

            _mapper.Map(command, existingSale).Returns(updatedSale);

            _saleRepository.UpdateAsync(existingSale, Arg.Any<CancellationToken>())
                           .Returns(updatedSale);

            _mapper.Map<UpdateSaleResult>(updatedSale).Returns(updateSaleResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Sale.Should().NotBeNull();
            result.Sale.Id.Should().Be(existingSale.Id);
            // Verifica se o SaleNumber não mudou
            result.Sale.SaleNumber.Should().Be("OriginalNumber");

            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _saleRepository.Received(1).UpdateAsync(existingSale, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map(command, existingSale);
            _mapper.Received(1).Map<UpdateSaleResult>(updatedSale);
        }

        [Fact(DisplayName = "Given an invalid UpdateSaleCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateInvalidCommand();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given a non-existent sale Id, when Handle is invoked, then it throws InvalidOperationException")]
        public async Task Handle_NonExistentSale_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Sale?)null);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Sale with Id {command.Id} does not exist.");
        }

        [Fact(DisplayName = "Given a valid command, when Handle is invoked, then it maps command to the existing Sale entity")]
        public async Task Handle_ValidRequest_MapsCommandToSale()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            var existingSale = new Sale(
                "OriginalNumber",
                DateTime.Now.AddDays(-2),
                Guid.NewGuid(),
                Guid.NewGuid()
            )
            {
                Id = command.Id
            };

            var updatedSale = new Sale(
                "OriginalNumber", // Mantém o mesmo SaleNumber
                command.SaleDate,
                command.Customer,
                command.Branch
            )
            {
                Id = command.Id
            };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingSale);

            _mapper.Map(command, existingSale).Returns(updatedSale);
            _saleRepository.UpdateAsync(updatedSale, Arg.Any<CancellationToken>())
                           .Returns(updatedSale);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mapper.Received(1).Map(command, existingSale);
        }
    }
}