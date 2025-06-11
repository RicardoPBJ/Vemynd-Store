using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Repositories;
using Xunit;

namespace VemyndStore.Api.Tests.Integration.Repositories
{
    /// <summary>
    /// Testes de integração para ProductRepository.
    /// Foca nas responsabilidades específicas do repository: acesso a dados e operações básicas do EF Core.
    /// </summary>
    public class ProductRepositoryTests
    {
        #region Setup

        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"ProductRepositoryTestDb_{Guid.NewGuid()}")
                .Options;
            return new ApplicationDbContext(options);
        }

        private Product CreateValidProduct(string? name = null)
        {
            return new Product
            {
                Name = name ?? $"Produto Teste {Guid.NewGuid()}",
                Description = "Descrição teste",
                Price = 100.00m,
                Brand = "Test Brand",
                Model = "Test Model"
            };
        }

        #endregion

        #region GetByIdAsync Tests

        /// <summary>
        /// Testa recuperação de produto por ID existente.
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldReturnProduct()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            var product = CreateValidProduct();
            await repo.AddAsync(product);

            // Act
            var result = await repo.GetByIdAsync(product.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
        }

        /// <summary>
        /// Testa recuperação com ID inexistente.
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_NonExistentId_ShouldReturnNull()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);

            // Act
            var result = await repo.GetByIdAsync(9999);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region GetAllAsync Tests

        /// <summary>
        /// Testa recuperação de todos os produtos.
        /// </summary>
        [Fact]
        public async Task GetAllAsync_WithProducts_ShouldReturnAllProducts()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            var product1 = CreateValidProduct("Produto 1");
            var product2 = CreateValidProduct("Produto 2");
            
            await repo.AddAsync(product1);
            await repo.AddAsync(product2);

            // Act
            var results = await repo.GetAllAsync();
            var productsList = results.ToList();

            // Assert
            productsList.Should().HaveCount(2);
            productsList.Should().Contain(p => p.Name == "Produto 1");
            productsList.Should().Contain(p => p.Name == "Produto 2");
        }

        /// <summary>
        /// Testa GetAllAsync com banco vazio.
        /// </summary>
        [Fact]
        public async Task GetAllAsync_EmptyDatabase_ShouldReturnEmptyCollection()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);

            // Act
            var results = await repo.GetAllAsync();

            // Assert
            results.Should().BeEmpty();
        }

        #endregion

        #region AddAsync Tests

        /// <summary>
        /// Testa adição de produto válido.
        /// </summary>
        [Fact]
        public async Task AddAsync_ValidProduct_ShouldAddSuccessfully()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            var product = CreateValidProduct();

            // Act
            await repo.AddAsync(product);

            // Assert
            product.Id.Should().BeGreaterThan(0, "ID deve ser gerado pelo banco");
            
            var saved = await repo.GetByIdAsync(product.Id);
            saved.Should().NotBeNull();
            saved!.Name.Should().Be(product.Name);
        }

        #endregion

        #region UpdateAsync Tests

        /// <summary>
        /// Testa atualização de produto existente.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ExistingProduct_ShouldUpdateSuccessfully()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            var product = CreateValidProduct();
            await repo.AddAsync(product);

            // Act
            product.Name = "Nome Atualizado";
            product.Price = 200.00m;
            await repo.UpdateAsync(product);

            // Assert
            var updated = await repo.GetByIdAsync(product.Id);
            updated.Should().NotBeNull();
            updated!.Name.Should().Be("Nome Atualizado");
            updated.Price.Should().Be(200.00m);
        }

        #endregion

        #region DeleteAsync Tests

        /// <summary>
        /// Testa exclusão de produto existente.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ExistingProduct_ShouldRemoveProduct()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            var product = CreateValidProduct();
            await repo.AddAsync(product);

            // Act
            await repo.DeleteAsync(product);

            // Assert
            var deleted = await repo.GetByIdAsync(product.Id);
            deleted.Should().BeNull();
        }

        #endregion

        #region ExistsByNameAsync Tests

        /// <summary>
        /// Testa verificação de existência por nome - caso positivo.
        /// </summary>
        [Fact]
        public async Task ExistsByNameAsync_ExistingName_ShouldReturnTrue()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            const string productName = "Produto Específico";
            await repo.AddAsync(CreateValidProduct(productName));

            // Act
            var exists = await repo.ExistsByNameAsync(productName);

            // Assert
            exists.Should().BeTrue();
        }

        /// <summary>
        /// Testa verificação de existência por nome - caso negativo.
        /// </summary>
        [Fact]
        public async Task ExistsByNameAsync_NonExistentName_ShouldReturnFalse()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);

            // Act
            var exists = await repo.ExistsByNameAsync("Nome Inexistente");

            // Assert
            exists.Should().BeFalse();
        }

        /// <summary>
        /// Testa se a verificação de nome é case-sensitive (conforme implementação atual).
        /// Se precisar de case-insensitive, deve ser implementado no repository.
        /// </summary>
        [Fact]
        public async Task ExistsByNameAsync_CaseSensitive_ShouldMatchExactCase()
        {
            // Arrange
            using var db = GetDbContext();
            var repo = new ProductRepository(db);
            const string productName = "ProdutoTeste";
            await repo.AddAsync(CreateValidProduct(productName));

            // Act
            var exactMatch = await repo.ExistsByNameAsync("ProdutoTeste");
            var differentCase = await repo.ExistsByNameAsync("produtoteste");

            // Assert
            exactMatch.Should().BeTrue("deve encontrar nome exato");
            differentCase.Should().BeFalse("implementação atual é case-sensitive");
        }

        #endregion
    }
}