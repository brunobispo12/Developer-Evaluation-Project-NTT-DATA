﻿namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Obtém o identificador único do usuário.
        /// </summary>
        /// <returns>O ID do usuário como uma string.</returns>
        public string Id { get; }

        public string FirstName { get; }
        public string LastName { get; }

        /// <summary>
        /// Obtém o papel/função do usuário no sistema.
        /// </summary>
        /// <returns>O papel do usuário como uma string.</returns>
        public string Role { get; }
    }
}
