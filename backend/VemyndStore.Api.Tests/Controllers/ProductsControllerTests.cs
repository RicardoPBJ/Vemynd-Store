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

/// <summary>
/// Classe de testes para o controlador de produtos (ProductsController).
/// Testa o endpoint POST /api/products.
/// </summary>
public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    /// <summary>
    /// Configura o WebApplicationFactory para usar um banco de dados InMemory.
    /// </summary>
    /// <param name="factory">Instância do WebApplicationFactory.</param>
    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Substitui o DbContext pelo InMemoryDatabase para testes
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDatabase"));
            });
        });
    }

    /// <summary>
    /// Testa a criação de um produto válido.
    /// Verifica se o status retornado é 201 Created e se os dados retornados correspondem aos enviados.
    /// </summary>
    [Fact]
    public async Task PostProduct_ValidProduct_ReturnsCreated()
    {
        // Arrange: Configura o cliente HTTP e o produto válido a ser enviado.
        var client = _factory.CreateClient();
        var newProduct = new Product
        {
            Name = "Test Laptop",
            Description = "A high-performance laptop for testing.",
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

        // Act: Envia o produto para o endpoint POST /api/products.
        var response = await client.PostAsJsonAsync("/api/products", newProduct);

        // Assert: Verifica se o status é 201 Created e se os dados retornados estão corretos.
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
    /// Verifica se o status retornado é 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task PostProduct_InvalidProduct_ReturnsBadRequest()
    {
        // Arrange: Configura o cliente HTTP e o produto inválido (sem o campo 'Name').
        var client = _factory.CreateClient();
        var invalidProduct = new Product
        {
            // Dados inválidos: falta o campo obrigatório 'Name'
            Description = "A high-performance laptop for testing.",
            Price = 1500.99m,
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

        // Act: Envia o produto inválido para o endpoint POST /api/products.
        var response = await client.PostAsJsonAsync("/api/products", invalidProduct);

        // Assert: Verifica se o status é 400 Bad Request.
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}