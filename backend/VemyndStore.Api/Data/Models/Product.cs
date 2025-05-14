using System.ComponentModel.DataAnnotations;

namespace VemyndStore.Api.Data.Models
{
    /// <summary>
    /// Representa um produto da loja, incluindo especificações técnicas e informações comerciais.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto (obrigatório).
        /// </summary>
        [Required(ErrorMessage = "O campo 'Name' é obrigatório.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do produto (opcional).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Preço do produto (obrigatório, deve ser maior que zero).
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo 'Price' deve ser maior que zero.")]
        public decimal Price { get; set; } = 0.0m;

        /// <summary>
        /// URL da imagem do produto (opcional).+-.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Marca do produto (opcional).
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// Modelo do produto (opcional).
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// Processador (opcional).
        /// </summary>
        public string? Processor { get; set; }

        /// <summary>
        /// Geração do processador (opcional).
        /// </summary>
        public string? ProcessorGeneration { get; set; }

        /// <summary>
        /// Capacidade de RAM (opcional).
        /// </summary>
        public string? Ram { get; set; }

        /// <summary>
        /// Tipo de armazenamento (opcional).
        /// </summary>
        public string? StorageType { get; set; }

        /// <summary>
        /// Capacidade de armazenamento (opcional).
        /// </summary>
        public string? StorageCapacity { get; set; }

        /// <summary>
        /// Placa de vídeo (opcional).
        /// </summary>
        public string? GraphicsCard { get; set; }

        /// <summary>
        /// Sistema operacional (opcional).
        /// </summary>
        public string? OperatingSystem { get; set; }

        /// <summary>
        /// Tamanho da tela (opcional).
        /// </summary>
        public string? DisplaySize { get; set; }

        /// <summary>
        /// Resolução da tela (opcional).
        /// </summary>
        public string? DisplayResolution { get; set; }

        /// <summary>
        /// Se a tela é sensível ao toque (padrão: false).
        /// </summary>
        public bool IsTouchscreen { get; set; } = false;

        /// <summary>
        /// Se possui unidade óptica (padrão: false).
        /// </summary>
        public bool HasOpticalDrive { get; set; } = false;

        /// <summary>
        /// Conectividade (opcional).
        /// </summary>
        public string? Connectivity { get; set; }

        /// <summary>
        /// Peso do produto (obrigatório, deve ser maior que zero).
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo 'Weight' deve ser maior que zero.")]
        public decimal Weight { get; set; } = 0.0m;

        /// <summary>
        /// Data de lançamento (obrigatório, deve ser maior que 01/01/2000).
        /// </summary>
        [Required(ErrorMessage = "O campo 'ReleaseDate' é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;
    }
}