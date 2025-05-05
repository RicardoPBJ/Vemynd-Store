using DotNetEnv;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data;

public class Program // Torne a classe Program pública
{
    public static void Main(string[] args)
    {
        // Carrega as variáveis de ambiente do arquivo .env
        DotNetEnv.Env.Load("/root/projetos/vemynd-store/.env");

        // Criação do builder para configurar e construir o aplicativo web.
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona serviços ao contêiner de injeção de dependência.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
 
        // Configuração da string de conexão com o banco de dados.
        var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Port=3306;Database={Environment.GetEnvironmentVariable("DB_DATABASE")};Uid={Environment.GetEnvironmentVariable("DB_USER")};Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

        // Testa a conexão com o banco de dados para garantir que está funcionando.
        try
        {
            using var connection = new MySqlConnector.MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Conexão com o banco de dados bem-sucedida!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            Environment.Exit(1); // Encerra o aplicativo se a conexão falhar.
        }

        // Configuração do DbContext para acesso ao banco de dados.
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Constrói o aplicativo com base nas configurações definidas.
        var app = builder.Build();

        // Configuração do pipeline de requisições HTTP.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}