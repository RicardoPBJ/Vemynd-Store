using System;
using VemyndStore.Api.Exceptions;
using Xunit;

namespace VemyndStore.Api.Tests.Exceptions
{
    /// <summary>
    /// Testes unitários para a BusinessException.
    /// Garante que a exceção personalizada de negócio funciona corretamente.
    /// </summary>
    public class BusinessExceptionTests
    {
        /// <summary>
        /// Deve criar uma BusinessException com a mensagem correta.
        /// </summary>
        [Fact]
        public void Deve_Criar_BusinessException_Com_Mensagem()
        {
            var ex = new BusinessException("Erro de negócio");
            Assert.Equal("Erro de negócio", ex.Message);
        }

        /// <summary>
        /// Deve criar uma BusinessException com mensagem e exceção interna.
        /// </summary>
        [Fact]
        public void Deve_Criar_BusinessException_Com_InnerException()
        {
            var inner = new Exception("Erro interno");
            var ex = new BusinessException("Erro de negócio", inner);
            Assert.Equal("Erro de negócio", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}