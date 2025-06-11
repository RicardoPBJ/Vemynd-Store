using Microsoft.AspNetCore.Mvc;
using VemyndStore.Api.Exceptions; // Assumindo que BusinessException está neste namespace

namespace VemyndStore.Api.Controllers
{
    /// <summary>
    /// Controller usado exclusivamente para testes de middleware.
    /// Este controller deve ser incluído apenas em ambiente de teste.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Endpoint que lança BusinessException para testar o middleware.
        /// </summary>
        [HttpGet("business-exception")]
        public IActionResult ThrowBusinessException()
        {
            throw new BusinessException("Erro de negócio para teste");
        }

        /// <summary>
        /// Endpoint que lança Exception geral para testar o middleware.
        /// </summary>
        [HttpGet("general-exception")]
        public IActionResult ThrowGeneralException()
        {
            throw new Exception("Erro geral para teste");
        }

        /// <summary>
        /// Endpoint que retorna sucesso para verificar se o controller está funcionando.
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { message = "Test controller is working" });
        }
    }
}