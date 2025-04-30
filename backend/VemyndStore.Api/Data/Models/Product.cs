namespace VemyndStore.Api.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; } // Marca do computador (Dell, HP, Lenovo, etc.)
        public string Model { get; set; } // Modelo específico (XPS 15, IdeaPad Slim 5, etc.)
        public string Processor { get; set; }
        public string ProcessorGeneration { get; set; } // Geração do processador (ex: 13ª Geração Intel Core)
        public string Ram { get; set; } // Capacidade e tipo (ex: 16GB DDR5)
        public string StorageType { get; set; } // Tipo de armazenamento (SSD, HDD)
        public string StorageCapacity { get; set; } // Capacidade de armazenamento (ex: 512GB, 1TB)
        public string GraphicsCard { get; set; }
        public string OperatingSystem { get; set; }
        public string DisplaySize { get; set; } // Tamanho da tela (ex: 15.6 polegadas)
        public string DisplayResolution { get; set; } // Resolução da tela (ex: Full HD, 4K)
        public bool IsTouchscreen { get; set; } // Se a tela é sensível ao toque
        public bool HasOpticalDrive { get; set; } // Se possui unidade óptica (CD/DVD)
        public string Connectivity { get; set; } // Portas e conectividade (USB, HDMI, Wi-Fi, Bluetooth)
        public decimal Weight { get; set; } // Peso do computador
        public DateTime ReleaseDate { get; set; } // Data de lançamento
    }
}