using Microsoft.AspNetCore.Mvc;
using VemyndStore.Api.Data;
using VemyndStore.Api.Data.Models;

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
    /// <returns>Status 201 Created se o produto for criado com sucesso, ou 400 Bad Request se os dados forem inválidos.</returns>
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        // Validação de dados
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest("O campo 'Name' é obrigatório.");
        }

        if (product.Price <= 0)
        {
            return BadRequest("O campo 'Price' deve ser maior que zero.");
        }

        // Adiciona o produto ao banco de dados
        _context.Products.Add(product);
        _context.SaveChanges();

        // Retorna 201 Created com o produto criado
        return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
    }
}