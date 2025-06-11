using VemyndStore.Api.Data.Models;

namespace VemyndStore.Api.Repositories.Interfaces
{
    /// <summary>
    /// Contrato para operações de acesso a dados de produtos.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Busca um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Produto encontrado ou null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Adiciona um novo produto ao banco.
        /// </summary>
        /// <param name="product">Produto a ser adicionado.</param>
        Task AddAsync(Product product);

        /// <summary>
        /// Atualiza um produto existente no banco.
        /// </summary>
        /// <param name="product">Produto atualizado.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Remove um produto do banco.
        /// </summary>
        /// <param name="product">Produto a ser removido.</param>
        Task DeleteAsync(Product product);

        /// <summary>
        /// Verifica se já existe um produto com o nome informado.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        /// <returns>True se existir, false caso contrário.</returns>
        Task<bool> ExistsByNameAsync(string name);
    }
}