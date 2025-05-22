using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VemyndStore.Api.Exceptions;

namespace VemyndStore.Api.Middleware
{
    /// <summary>
    /// Middleware para tratamento global de exceções de negócio (BusinessException).
    /// Intercepta exceções lançadas durante o processamento das requisições e retorna uma resposta padronizada.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Construtor do middleware.
        /// </summary>
        /// <param name="next">Delegate para o próximo middleware no pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método chamado para processar a requisição HTTP.
        /// </summary>
        /// <param name="context">Contexto da requisição HTTP.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);// Chama o próximo middleware no pipeline
            }
            catch (BusinessException ex)
            {
                // Trata exceções de negócio e retorna resposta padronizada
                context.Response.StatusCode = 422; // Unprocessable Entity
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
            }
            catch (Exception)
            {
                // Trata outras exceções não previstas
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Ocorreu um erro inesperado.\"}");
            }
        }
    }
}