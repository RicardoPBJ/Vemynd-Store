using System.Collections.Generic;
using System.Threading.Tasks;
using VemyndStore.Api.Data.Models;
using VemyndStore.Api.Exceptions;
using VemyndStore.Api.Repositories.Interfaces;

namespace VemyndStore.Api.Services
{
    /// <summary>
    /// Implementação das regras de negócio para produtos.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Construtor do serviço de produtos.
        /// </summary>
        /// <param name="repository">Repositório de produtos.</param>
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<Product> CreateAsync(Product product)
        {
            // Regra de negócio: não permitir produtos com nome duplicado
            if (await _repository.ExistsByNameAsync(product.Name))
                throw new BusinessException("Já existe um produto com esse nome cadastrado.");

            // Adiciona o produto ao banco
            await _repository.AddAsync(product);
            return product;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // Retorna todos os produtos cadastrados
            return await _repository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task<Product?> GetByIdAsync(int id)
        {
            // Busca um produto pelo ID, retorna null se não existir
            return await _repository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Product> UpdateAsync(Product product)
        {
            // Busca o produto existente
            var existing = await _repository.GetByIdAsync(product.Id);
            if (existing == null)
                throw new BusinessException("Produto não encontrado.");

            // Regra de negócio: não permitir nome duplicado ao atualizar
            if (existing.Name != product.Name &&
                await _repository.ExistsByNameAsync(product.Name))
                throw new BusinessException("Já existe um produto com esse nome cadastrado.");

            // Atualiza os campos do produto existente
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.ImageUrl = product.ImageUrl;
            existing.Brand = product.Brand;
            existing.Model = product.Model;
            existing.Processor = product.Processor;
            existing.ProcessorGeneration = product.ProcessorGeneration;
            existing.Ram = product.Ram;
            existing.StorageType = product.StorageType;
            existing.StorageCapacity = product.StorageCapacity;
            existing.GraphicsCard = product.GraphicsCard;
            existing.OperatingSystem = product.OperatingSystem;
            existing.DisplaySize = product.DisplaySize;
            existing.DisplayResolution = product.DisplayResolution;
            existing.IsTouchscreen = product.IsTouchscreen;
            existing.HasOpticalDrive = product.HasOpticalDrive;
            existing.Connectivity = product.Connectivity;
            existing.Weight = product.Weight;
            existing.ReleaseDate = product.ReleaseDate;

            // Salva as alterações no banco
            await _repository.UpdateAsync(existing);
            return existing;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            // Busca o produto pelo ID
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return false;

            // Remove o produto do banco
            await _repository.DeleteAsync(product);
            return true;
        }
    }
}