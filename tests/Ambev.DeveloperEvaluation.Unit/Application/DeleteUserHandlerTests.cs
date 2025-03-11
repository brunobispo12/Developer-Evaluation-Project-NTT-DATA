using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository);
        }

        [Fact(DisplayName = "Given a valid DeleteSaleCommand, when Handle is invoked, then it returns a success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand { Id = saleId };

            _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            await _saleRepository.Received(1).DeleteAsync(saleId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given a non-existing sale, when Handle is invoked, then it throws KeyNotFoundException")]
        public async Task Handle_NonExistingSale_ThrowsKeyNotFoundException()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = new DeleteSaleCommand { Id = saleId };

            _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(false));

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {saleId} was not found.");
        }

        [Fact(DisplayName = "Given an invalid DeleteSaleCommand, when Handle is invoked, then it throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = new DeleteSaleCommand { Id = Guid.Empty };

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}