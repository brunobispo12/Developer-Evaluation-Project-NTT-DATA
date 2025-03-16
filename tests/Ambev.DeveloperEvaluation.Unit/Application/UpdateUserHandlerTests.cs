using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="UpdateUserHandler"/>.
    /// </summary>
    public class UpdateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UpdateUserHandler _handler;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _handler = new UpdateUserHandler(_userRepository, _mapper, _passwordHasher);
        }

        [Fact(DisplayName = "Given valid update user command, when Handle is invoked, then returns success result")]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateValidCommand();
            var existingUser = new User
            {
                Id = command.Id,
                Name = new Name { Firstname = "OldFirst", Lastname = "OldLast" },
                Email = "old@example.com",
                Phone = "+5511988888888",
                Status = UserStatus.Suspended,
                Role = UserRole.Admin,
                Address = new Address
                {
                    City = "Old City",
                    Street = "Old Street",
                    Number = 1,
                    Zipcode = "00000-000",
                    Geolocation = new Geolocation { Lat = "0", Long = "0" }
                }
            };

            var updatedUser = new User
            {
                Id = command.Id,
                Name = new Name { Firstname = command.FirstName, Lastname = command.LastName },
                Email = command.Email,
                Phone = command.Phone,
                Status = command.Status,
                Role = command.Role,
                Address = new Address
                {
                    City = command.Address.City,
                    Street = command.Address.Street,
                    Number = command.Address.Number,
                    Zipcode = command.Address.Zipcode,
                    Geolocation = new Geolocation { Lat = command.Address.Geolocation.Lat, Long = command.Address.Geolocation.Long }
                }
            };

            var resultDto = new UpdateUserResult
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                Phone = updatedUser.Phone,
                Status = updatedUser.Status,
                Role = updatedUser.Role,
                FirstName = updatedUser.Name.Firstname,
                LastName = updatedUser.Name.Lastname,
                Address = new UpdateUserAddressResult
                {
                    City = updatedUser.Address.City,
                    Street = updatedUser.Address.Street,
                    Number = updatedUser.Address.Number,
                    Zipcode = updatedUser.Address.Zipcode,
                    Geolocation = new UpdateUserGeolocationResult
                    {
                        Lat = updatedUser.Address.Geolocation.Lat,
                        Long = updatedUser.Address.Geolocation.Long
                    }
                }
            };

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingUser);
            _mapper.Map(command, existingUser).Returns(updatedUser);
            _userRepository.UpdateAsync(existingUser, Arg.Any<CancellationToken>()).Returns(updatedUser);
            _mapper.Map<UpdateUserResult>(updatedUser).Returns(resultDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.Email.Should().Be(command.Email);
            result.FirstName.Should().Be(command.FirstName);
            result.Address.City.Should().Be(command.Address.City);

            await _userRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _userRepository.Received(1).UpdateAsync(existingUser, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map(command, existingUser);
            _mapper.Received(1).Map<UpdateUserResult>(updatedUser);
        }

        [Fact(DisplayName = "Given invalid update user command, when Handle is invoked, then throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateInvalidCommand();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}