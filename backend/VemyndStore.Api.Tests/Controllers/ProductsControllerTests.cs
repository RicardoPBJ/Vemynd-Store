using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VemyndStore.Api.Data;
using FluentAssertions; // Supondo que você usa FluentAssertions

/// <summary>
/// Classe de testes de integração para o ProductsController.
/// Garante que todos os endpoints funcionem corretamente com Service e Repository.
/// </summary>
public class ProductsControllerTests
{

    /// <summary>
    /// Construtor: Configura o ambiente, se necessário.
    /// </summary>
    public ProductsControllerTests()
    {
        // Define o ambiente de teste uma única vez para toda a suíte.
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }

    /// <summary>
    /// Método auxiliar para criar um factory com banco isolado para testes específicos.
    /// </summary>
    private WebApplicationFactory<Program> CreateConfiguredFactory(string dbName)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
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

                    // Usa o nome do banco específico para esta instância
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase(dbName));
                });
            });
    }

    /// <summary>
    /// Testa a criação de um produto válido.
    /// </summary>
    [Fact]
    public async Task PostProduct_ValidProduct_ReturnsCreated()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();
        var newProduct = ProdutoValido();

        var response = await client.PostAsJsonAsync("/api/products", newProduct);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
        createdProduct.Should().NotBeNull();
        createdProduct!.Name.Should().Be(newProduct.Name);
        createdProduct.Description.Should().Be(newProduct.Description);
        createdProduct.Price.Should().Be(newProduct.Price);
        createdProduct.Brand.Should().Be(newProduct.Brand);
        createdProduct.Model.Should().Be(newProduct.Model);
    }

    /// <summary>
    /// Testa o envio de um produto inválido.
    /// </summary>
    [Fact]
    public async Task PostProduct_InvalidProduct_ReturnsBadRequest()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();
        var invalidProduct = ProdutoValido();
        invalidProduct.Name = ""; // Inválido

        var response = await client.PostAsJsonAsync("/api/products", invalidProduct);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Testa o envio de um produto duplicado.
    /// </summary>
    [Fact]
    public async Task PostProduct_DuplicateName_ReturnsUnprocessableEntity()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}"); // Garante isolamento
        var client = factory.CreateClient();
        var product = ProdutoValido();

        // Cria o produto pela primeira vez
        var firstResponse = await client.PostAsJsonAsync("/api/products", product);
        firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Tenta criar novamente com o mesmo nome
        var response = await client.PostAsJsonAsync("/api/products", product);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity); // Usando a enumeração
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Já existe um produto com esse nome cadastrado.");
    }

    /// <summary>
    /// Testa a busca de todos os produtos.
    /// </summary>
    [Fact]
    public async Task GetAllProducts_ReturnsOkAndList()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();

        // Adiciona um produto e verifica se foi criado
        var postResponse = await client.PostAsJsonAsync("/api/products", ProdutoValido());
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var response = await client.GetAsync("/api/products");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<Product[]>();
        products.Should().NotBeNull();
        products!.Should().HaveCountGreaterThanOrEqualTo(1, "Deve haver pelo menos um produto cadastrado.");
    }

    /// <summary>
    /// Testa a busca de um produto por ID.
    /// </summary>
    [Fact]
    public async Task GetProductById_ReturnsOkOrNotFound()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();

        // Adiciona um produto
        var postResponse = await client.PostAsJsonAsync("/api/products", ProdutoValido());
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await postResponse.Content.ReadFromJsonAsync<Product>();
        created.Should().NotBeNull();

        // Busca o produto criado
        var response = await client.GetAsync($"/api/products/{created!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK,
            $"Produto com ID {created.Id} deveria existir");

        // Busca um ID inexistente
        var notFoundResponse = await client.GetAsync($"/api/products/9999");
        notFoundResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Testa a atualização de um produto existente.
    /// </summary>
    [Fact]
    public async Task UpdateProduct_ReturnsOk()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();

        var postResponse = await client.PostAsJsonAsync("/api/products", ProdutoValido());
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await postResponse.Content.ReadFromJsonAsync<Product>();
        created.Should().NotBeNull();

        // Modifica o produto para ter um nome único (evita conflito de duplicação)
        created!.Name = $"Produto Atualizado {Guid.NewGuid()}";

        var putResponse = await client.PutAsJsonAsync($"/api/products/{created.Id}", created);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK,
            await putResponse.Content.ReadAsStringAsync());

        var updated = await putResponse.Content.ReadFromJsonAsync<Product>();
        updated.Should().NotBeNull();
        updated!.Name.Should().StartWith("Produto Atualizado");
    }

    /// <summary>
    /// Testa a remoção de um produto existente.
    /// </summary>
    [Fact]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        using var factory = CreateConfiguredFactory($"TestDb_{Guid.NewGuid()}");
        var client = factory.CreateClient();

        var postResponse = await client.PostAsJsonAsync("/api/products", ProdutoValido());
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await postResponse.Content.ReadFromJsonAsync<Product>();
        created.Should().NotBeNull();
        // Tenta deletar o produto criado
        var deleteResponse = await client.DeleteAsync($"/api/products/{created!.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent,
            $"Erro ao deletar produto ID {created.Id}: {await deleteResponse.Content.ReadAsStringAsync()}");

        // Confirma que não existe mais
        var getResponse = await client.GetAsync($"/api/products/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Cria um produto válido para ser usado nos testes.
    /// </summary>
    private Product ProdutoValido()
    {
        return new Product
        {
            Name = $"Produto Teste {Guid.NewGuid()}",
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