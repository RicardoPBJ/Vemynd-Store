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

        // Configuração explícita para HTTPS com certificado
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5000); // HTTP
            options.ListenAnyIP(5001, listenOptions =>
            {
                // Configuração de HTTPS usando o certificado PFX
                listenOptions.UseHttps(httpsOptions =>
                {
                    var certPath = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path") 
                        ?? "/root/.aspnet/https/aspnetapp.pfx";
                    var certPassword = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password") 
                        ?? "certifypsw";
                    
                    httpsOptions.ServerCertificate = new X509Certificate2(certPath, certPassword);
                });
            });
        });

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