using System.Collections.Generic;
using System.Threading.Tasks;
using VemyndStore.Api.Data.Models;

namespace VemyndStore.Api.Services
{
    /// <summary>
    /// Interface para operações de negócio relacionadas a produtos.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">Produto a ser criado.</param>
        /// <returns>Produto criado.</returns>
        Task<Product> CreateAsync(Product product);

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Produto encontrado ou null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="product">Produto atualizado.</param>
        /// <returns>Produto atualizado.</returns>
        Task<Product> UpdateAsync(Product product);

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>True se removido, false se não encontrado.</returns>
        Task<bool> DeleteAsync(int id);
    }
}