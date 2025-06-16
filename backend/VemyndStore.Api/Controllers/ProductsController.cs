using Microsoft.AspNetCore.Mvc;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    /// <summary>
    /// Construtor do controller de produtos.
    /// </summary>
    /// <param name="service">Serviço de produtos.</param>
    public ProductsController(IProductService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="product">Produto enviado no corpo da requisição.</param>
    /// <returns>Status 201 Created se o produto for criado com sucesso, 422 se regra de negócio for violada, ou 400 se dados inválidos.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        // Validação automática: [ApiController] + FluentValidation retornam 400 se inválido
        var createdProduct = await _service.CreateAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    /// <summary>
    /// Retorna todos os produtos cadastrados.
    /// </summary>
    /// <returns>Lista de produtos.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Retorna um produto pelo ID.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <returns>Produto encontrado ou 404 se não existir.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <param name="product">Produto atualizado.</param>
    /// <returns>Produto atualizado ou 404 se não existir.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest("ID do produto não confere com o corpo da requisição.");

        var updated = await _service.UpdateAsync(product);
        return Ok(updated);
    }

    /// <summary>
    /// Remove um produto pelo ID.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <returns>Status 204 No Content se removido, 404 se não encontrado.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var removed = await _service.DeleteAsync(id);
        if (!removed)
            return NotFound();
        return NoContent();
    }
}