using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VemyndStore.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Processor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorGeneration = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ram = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageCapacity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GraphicsCard = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperatingSystem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplaySize = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayResolution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsTouchscreen = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasOpticalDrive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Connectivity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Connectivity", "Description", "DisplayResolution", "DisplaySize", "GraphicsCard", "HasOpticalDrive", "ImageUrl", "IsTouchscreen", "Model", "Name", "OperatingSystem", "Price", "Processor", "ProcessorGeneration", "Ram", "ReleaseDate", "StorageCapacity", "StorageType", "Weight" },
                values: new object[,]
                {
                    { 1, "Dell", "Thunderbolt 4, USB 3.2, HDMI, Wi-Fi 6E, Bluetooth 5.2", "Potente notebook com tela InfinityEdge e processador de última geração.", "Full HD", "15.6 polegadas", "NVIDIA GeForce RTX 3050", false, "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/notebooks/inspiron-notebooks/15-3520/media-gallery/in3520-xnb-01-bk.psd?fmt=png-alpha&pscan=auto&scl=1&wid=5000&hei=5000&qlt=100,1&resMode=sharp2&size=5000,5000&chrss=full&imwidth=5000", false, "XPS 15", "Notebook Dell XPS 15", "Windows 11", 1799.99m, "Intel Core i7", "13ª Geração", "16GB DDR5", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "512GB", "SSD", 1.87m },
                    { 2, "Alienware", "USB 3.2, USB 2.0, Ethernet, Wi-Fi 6, Bluetooth 5.0", "Desktop de alta performance para jogos com design futurista.", "", "", "NVIDIA GeForce RTX 3080", false, "https://i.dell.com/is/image/DellContent/content/dam/ss2/product-images/dell-client-products/desktops/alienware-desktops/alienware-aurora-r14/media-gallery/dark-side-of-the-moon-bk-clear-panel-clear-air-cooled-air/desktop_alienware_aurora_r14_bk_clear-panel_air_cooled_gallery_3.psd?fmt=png-alpha&pscan=auto&scl=1&wid=2413&hei=3935&qlt=100,1&resMode=sharp2&size=2413,3935&chrss=full&imwidth=5000", false, "Aurora R14", "Desktop Gamer Alienware Aurora R14", "Windows 11", 2599.00m, "AMD Ryzen 9", "Série 5000", "32GB DDR4", new DateTime(2022, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1TB + 2TB", "SSD + HDD", 12.1m },
                    { 3, "Lenovo", "USB-C, USB 3.2, HDMI, Wi-Fi 6, Bluetooth 5.1", "Notebook fino e leve para o dia a dia.", "Full HD", "14 polegadas", "Intel Iris Xe Graphics", false, "https://p3-ofp.static.pub//fes/cms/2024/09/12/5ztqhlpp7ivhmq4rix5lcoyt64hqnz824789.png", false, "IdeaPad Slim 5", "Notebook Lenovo IdeaPad Slim 5", "Windows 11", 749.99m, "Intel Core i5", "12ª Geração", "8GB DDR4", new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "256GB", "SSD", 1.39m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
