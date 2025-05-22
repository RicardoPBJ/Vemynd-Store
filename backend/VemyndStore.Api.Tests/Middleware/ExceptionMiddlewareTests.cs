using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace VemyndStore.Api.Tests.Middleware
{
    /// <summary>
    /// Testes de integração para o ExceptionMiddleware.
    /// Garante que exceções de negócio e gerais são tratadas e retornam o status e mensagem corretos.
    /// </summary>
    public class ExceptionMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ExceptionMiddlewareTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Deve retornar 422 e mensagem de erro ao lançar BusinessException.
        /// É necessário um endpoint de teste que lance BusinessException.
        /// </summary>
        [Fact]
        public async Task Deve_Retornar_422_Quando_BusinessException_For_Lancada()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/test/business-exception"); // Implemente esse endpoint para teste

            response.StatusCode.Should().Be((HttpStatusCode)422);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("error");
        }

        /// <summary>
        /// Deve retornar 500 e mensagem genérica ao lançar Exception.
        /// É necessário um endpoint de teste que lance Exception.
        /// </summary>
        [Fact]
        public async Task Deve_Retornar_500_Quando_Exception_Geral_For_Lancada()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/test/general-exception"); // Implemente esse endpoint para teste

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Ocorreu um erro inesperado");
        }
    }
}