using System;

namespace VemyndStore.Api.Exceptions
{
    /// <summary>
    /// Exceção para erros de regra de negócio.
    /// Use esta exceção para indicar que uma regra do domínio foi violada,
    /// permitindo tratamento diferenciado no pipeline da API.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Cria uma nova exceção de negócio com uma mensagem.
        /// </summary>
        /// <param name="message">Mensagem descritiva do erro de negócio.</param>
        public BusinessException(string message) : base(message) { }

        /// <summary>
        /// Cria uma nova exceção de negócio com uma mensagem e exceção interna.
        /// </summary>
        /// <param name="message">Mensagem descritiva do erro de negócio.</param>
        /// <param name="innerException">Exceção interna original.</param>
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}