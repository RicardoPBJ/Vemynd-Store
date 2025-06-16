using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VemyndStore.Api;
using VemyndStore.Api.Data;
using VemyndStore.Api.Data.Models;
using Xunit;
using FluentAssertions;
using System.Linq;

/// <summary>
/// Classe de testes para o ProductsController.
/// Garante que o endpoint POST /api/products funciona corretamente em cenários de sucesso, validação e regra de negócio.
/// </summary>
public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    /// <summary>
    /// Configura o WebApplicationFactory para usar um banco de dados InMemory único por teste e ambiente "Testing".
    /// </summary>
    /// <param name="factory">Instância do WebApplicationFactory.</param>
    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        // Garante que o ambiente será "Testing" para usar o banco InMemory
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove qualquer configuração anterior do DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Gera um banco InMemory único para cada teste, garantindo isolamento total
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Scoped);
            });
        });
    }

    /// <summary>
    /// Testa a criação de um produto válido.
    /// Espera status 201 Created e os dados retornados iguais aos enviados.
    /// </summary>
    [Fact]
    public async Task PostProduct_ValidProduct_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var newProduct = ProdutoValido();

        var response = await client.PostAsJsonAsync("/api/products", newProduct);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
        createdProduct.Should().NotBeNull();
        createdProduct.Name.Should().Be(newProduct.Name);
        createdProduct.Description.Should().Be(newProduct.Description);
        createdProduct.Price.Should().Be(newProduct.Price);
        createdProduct.Brand.Should().Be(newProduct.Brand);
        createdProduct.Model.Should().Be(newProduct.Model);
    }

    /// <summary>
    /// Testa o envio de um produto inválido (sem o campo obrigatório 'Name').
    /// Espera status 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task PostProduct_InvalidProduct_ReturnsBadRequest()
    {
        var client = _factory.CreateClient();
        var invalidProduct = ProdutoValido();
        invalidProduct.Name = ""; // Inválido

        var response = await client.PostAsJsonAsync("/api/products", invalidProduct);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Testa o envio de um produto duplicado (mesmo nome).
    /// Espera status 422 Unprocessable Entity e mensagem de erro adequada.
    /// </summary>
    [Fact]
    public async Task PostProduct_DuplicateName_ReturnsUnprocessableEntity()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("DuplicateTestDb"), ServiceLifetime.Scoped);
            });
        });

        // Use o factory local, não o _factory do construtor!
        var client = factory.CreateClient();
        var product = ProdutoValido();

        // Cria o produto pela primeira vez
        await client.PostAsJsonAsync("/api/products", product);

        // Tenta criar novamente com o mesmo nome
        var response = await client.PostAsJsonAsync("/api/products", product);

        response.StatusCode.Should().Be((HttpStatusCode)422);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Já existe um produto com esse nome cadastrado.");
    }

    /// <summary>
    /// Cria um produto válido para ser usado nos testes.
    /// </summary>
    /// <returns>Instância de Product preenchida com dados válidos.</returns>
    private Product ProdutoValido()
    {
        return new Product
        {
            Name = "Produto Teste",
            Description = "Descrição teste",
            Price = 1500.99m,
            ImageUrl = "https://example.com/laptop.jpg",
            Brand = "Test Brand",
            Model = "Test Model",
            Processor = "Intel Core i7",
            ProcessorGeneration = "13th Gen",
            Ram = "16GB DDR5",
            StorageType = "SSD",
            StorageCapacity = "1TB",
            GraphicsCard = "NVIDIA RTX 3060",
            OperatingSystem = "Windows 11",
            DisplaySize = "15.6 inches",
            DisplayResolution = "4K",
            IsTouchscreen = true,
            HasOpticalDrive = false,
            Connectivity = "Wi-Fi, Bluetooth, USB-C",
            Weight = 1.8m,
            ReleaseDate = new DateTime(2023, 5, 1)
        };
    }
}