﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Handler for processing UpdateUserCommand requests.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="passwordHasher">The password hasher</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Handles the UpdateUserCommand request.
        /// </summary>
        /// <param name="command">The update user command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An UpdateUserResult with the updated user details</returns>
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {command.Id} not found");

            _mapper.Map(command, user);

            if (!string.IsNullOrWhiteSpace(command.Password))
            {
                user.Password = _passwordHasher.HashPassword(command.Password);
            }

            var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);

            var result = _mapper.Map<UpdateUserResult>(updatedUser);
            return result;
        }
    }
}