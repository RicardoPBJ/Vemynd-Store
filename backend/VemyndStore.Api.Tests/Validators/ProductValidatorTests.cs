using System;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Validators;
using Xunit;
using FluentAssertions;

namespace VemyndStore.Api.Tests.Validators
{
    /// <summary>
    /// Testes unitários para o ProductValidator.
    /// Garante que as regras de validação do produto estejam corretas e funcionando conforme esperado.
    /// </summary>
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator = new ProductValidator();

        /// <summary>
        /// Deve falhar se o nome do produto for vazio.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_Nome_For_Vazio()
        {
            // Arrange
            var product = ProdutoValido();
            product.Name = "";

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Name");
        }

        /// <summary>
        /// Deve falhar se o preço do produto for menor ou igual a zero.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_Preco_For_Menor_Ou_Igual_A_Zero()
        {
            // Arrange
            var product = ProdutoValido();
            product.Price = 0;

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Price");
        }

        /// <summary>
        /// Deve falhar se a data de lançamento for anterior a 01/01/2001.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_ReleaseDate_For_Antes_De_2001()
        {
            // Arrange
            var product = ProdutoValido();
            product.ReleaseDate = new DateTime(1999, 12, 31);

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "ReleaseDate");
        }

        /// <summary>
        /// Deve falhar se o peso do produto for menor ou igual a zero.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_Weight_For_Menor_Ou_Igual_A_Zero()
        {
            // Arrange
            var product = ProdutoValido();
            product.Weight = 0;

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Weight");
        }

        /// <summary>
        /// Deve falhar se o produto for touchscreen e não tiver resolução de tela informada.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_Touchscreen_Sem_DisplayResolution()
        {
            // Arrange
            var product = ProdutoValido();
            product.IsTouchscreen = true;
            product.DisplayResolution = "";

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "DisplayResolution");
        }

        /// <summary>
        /// Deve falhar se o preço for maior que 10.000 e a marca não for informada.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_Preco_Maior_Que_10000_Sem_Brand()
        {
            // Arrange
            var product = ProdutoValido();
            product.Price = 15000;
            product.Brand = "";

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Brand");
        }

        /// <summary>
        /// Deve falhar se a URL da imagem for inválida.
        /// </summary>
        [Fact]
        public void Deve_Falhar_Se_ImageUrl_For_Invalida()
        {
            // Arrange
            var product = ProdutoValido();
            product.ImageUrl = "imagem_invalida.jpg";

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "ImageUrl");
        }

        /// <summary>
        /// Deve passar se todos os campos estiverem válidos.
        /// </summary>
        [Fact]
        public void Deve_Passar_Se_Tudo_Valido()
        {
            // Arrange
            var product = ProdutoValido();

            // Act
            var result = _validator.Validate(product);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        /// <summary>
        /// Cria um produto válido para ser usado nos testes.
        /// </summary>
        private Product ProdutoValido()
        {
            return new Product
            {
                Name = "Produto Teste",
                Price = 100,
                ReleaseDate = new DateTime(2023, 1, 1),
                Weight = 1.5m,
                IsTouchscreen = false,
                DisplayResolution = "1920x1080",
                Brand = "Marca Teste",
                ImageUrl = "https://exemplo.com/imagem.jpg"
            };
        }
    }
}