using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static void Main(string[] args)
    {
        // Carrega as variáveis de ambiente do arquivo .env
        DotNetEnv.Env.Load();

        // Criação do builder para configurar e construir o aplicativo web.
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona serviços ao contêiner de injeção de dependência.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuração da string de conexão com o banco de dados.
        var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Port=3306;Database={Environment.GetEnvironmentVariable("DB_DATABASE")};Uid={Environment.GetEnvironmentVariable("DB_USER")};Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

        // Configuração do DbContext para acesso ao banco de dados.
        if (builder.Environment.IsEnvironment("Testing")) // Verifica se o ambiente é de testes
        {
            // Usa banco de dados em memória para testes
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
        }
        else
        {
            // Usa o banco de dados MySQL para desenvolvimento e produção
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        // Constrói o aplicativo com base nas configurações definidas.
        var app = builder.Build();

        // Configuração do pipeline de requisições HTTP.
        if (app.Environment.IsDevelopment())
        {
            // Habilita o Swagger apenas no ambiente de desenvolvimento
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Desativa o redirecionamento HTTPS no ambiente de testes
        if (!app.Environment.IsEnvironment("Testing"))
        {
            app.UseHttpsRedirection();
        }

        // Configura o middleware de autorização
        app.UseAuthorization();

        // Mapeia os controladores para os endpoints
        app.MapControllers();

        // Inicia o aplicativo
        app.Run();
    }
}