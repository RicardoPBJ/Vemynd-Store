namespace VemyndStore.Api.Data.Models
{
    public class Product
    {
        public int Id { get; set; } // Identificador único do produto
        public string Name { get; set; } = string.Empty; // Nome do produto (obrigatório)
        public string? Description { get; set; } // Descrição do produto (opcional)
        public decimal Price { get; set; } = 0.0m; // Preço do produto (obrigatório)
        public string? ImageUrl { get; set; } // URL da imagem do produto (opcional)
        public string? Brand { get; set; } // Marca do produto (opcional)
        public string? Model { get; set; } // Modelo do produto (opcional)
        public string? Processor { get; set; } // Processador (opcional)
        public string? ProcessorGeneration { get; set; } // Geração do processador (opcional)
        public string? Ram { get; set; } // Capacidade de RAM (opcional)
        public string? StorageType { get; set; } // Tipo de armazenamento (opcional)
        public string? StorageCapacity { get; set; } // Capacidade de armazenamento (opcional)
        public string? GraphicsCard { get; set; } // Placa de vídeo (opcional)
        public string? OperatingSystem { get; set; } // Sistema operacional (opcional)
        public string? DisplaySize { get; set; } // Tamanho da tela (opcional)
        public string? DisplayResolution { get; set; } // Resolução da tela (opcional)
        public bool IsTouchscreen { get; set; } = false; // Se a tela é sensível ao toque (padrão: false)
        public bool HasOpticalDrive { get; set; } = false; // Se possui unidade óptica (padrão: false)
        public string? Connectivity { get; set; } // Conectividade (opcional)
        public decimal Weight { get; set; } = 0.0m; // Peso do produto (obrigatório)
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue; // Data de lançamento (obrigatório)
    }
}