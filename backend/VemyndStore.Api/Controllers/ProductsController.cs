using Microsoft.AspNetCore.Mvc;
using VemyndStore.Api.Data;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Exceptions;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="product">Produto enviado no corpo da requisição.</param>
    /// <returns>Status 201 Created se o produto for criado com sucesso, 422 se regra de negócio for violada, ou 400 se dados inválidos.</returns>
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        // Validação automática: [ApiController] + FluentValidation retornam 400 se inválido

        // Regra de negócio: não permitir produtos com o mesmo nome
        if (_context.Products.Any(p => p.Name == product.Name))
        {
            throw new BusinessException("Já existe um produto com esse nome cadastrado.");
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
    }
}