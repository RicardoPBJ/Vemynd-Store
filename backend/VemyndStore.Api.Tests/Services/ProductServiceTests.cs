using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Exceptions;
using VemyndStore.Api.Repositories.Interfaces;
using VemyndStore.Api.Services;
using Xunit;

#region Testes de Produtos - ProductService

/// <summary>
/// Testes unitários para ProductService.
/// </summary>
public class ProductServiceTests
{
    #region Testes de Criação

    /// <summary>
    /// Garante que um produto é criado com sucesso quando não há duplicidade.
    /// </summary>
    [Fact]
    public async Task CreateAsync_DeveCriarProduto_QuandoNaoDuplicado()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        repoMock.Setup(r => r.ExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(false);
        repoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

        var service = new ProductService(repoMock.Object);
        var product = new Product { Name = "Novo Produto" };

        // Act
        var result = await service.CreateAsync(product);

        // Assert
        result.Should().Be(product);
        repoMock.Verify(r => r.AddAsync(product), Times.Once);
    }

    /// <summary>
    /// Garante que uma exceção é lançada ao tentar criar um produto duplicado.
    /// </summary>
    [Fact]
    public async Task CreateAsync_DeveLancarExcecao_QuandoDuplicado()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        repoMock.Setup(r => r.ExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(true);

        var service = new ProductService(repoMock.Object);
        var product = new Product { Name = "Duplicado" };

        // Act & Assert
        await Assert.ThrowsAsync<BusinessException>(() => service.CreateAsync(product));
    }

    #endregion

    #region Testes de Remoção

    /// <summary>
    /// Garante que um produto é removido com sucesso.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_DeveRemoverProduto_QuandoExiste()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var product = new Product { Id = 1 };
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
        repoMock.Setup(r => r.DeleteAsync(product)).Returns(Task.CompletedTask);

        var service = new ProductService(repoMock.Object);

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        result.Should().BeTrue();
        repoMock.Verify(r => r.DeleteAsync(product), Times.Once);
    }

    /// <summary>
    /// Garante que retorna false ao tentar remover um produto inexistente.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_DeveRetornarFalse_QuandoNaoExiste()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product?)null);

        var service = new ProductService(repoMock.Object);

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        result.Should().BeFalse();
    }
    #endregion
}

#endregion