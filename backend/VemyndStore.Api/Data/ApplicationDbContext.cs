using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data.Models;

namespace VemyndStore.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // DbSet para a entidade Product, representando a tabela de produtos no banco de dados.
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Notebook Dell XPS 15",
                    Description = "Potente notebook com tela InfinityEdge e processador de última geração.",
                    Price = 1799.99m,
                    ImageUrl = "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/inspiron-notebooks/15-3520/media-gallery/in3520-xnb-01-bk.psd?fmt=png-alpha&pscan=auto&scl=1&wid=5000&hei=5000&qlt=100,1&resMode=sharp2&size=5000,5000&chrss=full&imwidth=5000",
                    Brand = "Dell",
                    Model = "XPS 15",
                    Processor = "Intel Core i7",
                    ProcessorGeneration = "13ª Geração",
                    Ram = "16GB DDR5",
                    StorageType = "SSD",
                    StorageCapacity = "512GB",
                    GraphicsCard = "NVIDIA GeForce RTX 3050",
                    OperatingSystem = "Windows 11",
                    DisplaySize = "15.6 polegadas",
                    DisplayResolution = "Full HD",
                    IsTouchscreen = false,
                    HasOpticalDrive = false,
                    Connectivity = "Thunderbolt 4, USB 3.2, HDMI, Wi-Fi 6E, Bluetooth 5.2",
                    Weight = 1.87m,
                    ReleaseDate = new DateTime(2023, 9, 15)
                },
                new Product
                {
                    Id = 2,
                    Name = "Desktop Gamer Alienware Aurora R14",
                    Description = "Desktop de alta performance para jogos com design futurista.",
                    Price = 2599.00m,
                    ImageUrl = "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/desktops/alienware-desktops/alienware-aurora-r14/media-gallery/dark-side-of-the-moon-bk-clear-panel-clear-air-cooled-air/desktop_alienware_aurora_r14_bk_clear-panel_air_cooled_gallery_3.psd?fmt=png-alpha&pscan=auto&scl=1&wid=2413&hei=3935&qlt=100,1&resMode=sharp2&size=2413,3935&chrss=full&imwidth=5000",
                    Brand = "Alienware",
                    Model = "Aurora R14",
                    Processor = "AMD Ryzen 9",
                    ProcessorGeneration = "Série 5000",
                    Ram = "32GB DDR4",
                    StorageType = "SSD + HDD",
                    StorageCapacity = "1TB + 2TB",
                    GraphicsCard = "NVIDIA GeForce RTX 3080",
                    OperatingSystem = "Windows 11",
                    DisplaySize = "", // Não aplicável a desktops
                    DisplayResolution = "", // Não aplicável a desktops
                    IsTouchscreen = false,
                    HasOpticalDrive = false,
                    Connectivity = "USB 3.2, USB 2.0, Ethernet, Wi-Fi 6, Bluetooth 5.0",
                    Weight = 12.1m,
                    ReleaseDate = new DateTime(2022, 11, 20)
                },
                new Product
                {
                    Id = 3,
                    Name = "Notebook Lenovo IdeaPad Slim 5",
                    Description = "Notebook fino e leve para o dia a dia.",
                    Price = 749.99m,
                    ImageUrl = "https://p3-ofp.static.pub//fes/cms/2024/09/12/5ztqhlpp7ivhmq4rix5lcoyt64hqnz824789.png",
                    Brand = "Lenovo",
                    Model = "IdeaPad Slim 5",
                    Processor = "Intel Core i5",
                    ProcessorGeneration = "12ª Geração",
                    Ram = "8GB DDR4",
                    StorageType = "SSD",
                    StorageCapacity = "256GB",
                    GraphicsCard = "Intel Iris Xe Graphics",
                    OperatingSystem = "Windows 11",
                    DisplaySize = "14 polegadas",
                    DisplayResolution = "Full HD",
                    IsTouchscreen = false,
                    HasOpticalDrive = false,
                    Connectivity = "USB-C, USB 3.2, HDMI, Wi-Fi 6, Bluetooth 5.1",
                    Weight = 1.39m,
                    ReleaseDate = new DateTime(2023, 5, 10)
                }
            );
        }
    }
}
