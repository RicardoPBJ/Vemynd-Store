using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using VemyndStore.Api.Validators;
using VemyndStore.Api.Middleware;
using VemyndStore.Api.Repositories;
using VemyndStore.Api.Repositories.Interfaces;
using VemyndStore.Api.Services;
using VemyndStore.Api.Controllers;

public class Program
{
    /// <summary>
    /// Ponto de entrada da aplicação ASP.NET Core.
    /// Responsável por configurar serviços, middlewares e iniciar o servidor web.
    /// </summary>
    public static void Main(string[] args)
    {
        // Carrega as variáveis de ambiente do arquivo .env
        DotNetEnv.Env.Load("../../.env");

        // Criação do builder para configurar e construir o aplicativo web.
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona serviços ao contêiner de injeção de dependência
        builder.Services.AddControllers();

        // Adiciona serviços de validação com FluentValidation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<VemyndStore.Api.Validators.ProductValidator>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Registro do repositório e do service de produtos
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();

        // Configuração da string de conexão com o banco de dados.
        var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};Port=3306;Database={Environment.GetEnvironmentVariable("DB_DATABASE")};Uid={Environment.GetEnvironmentVariable("DB_USER")};Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

        // Configuração do DbContext para acesso ao banco de dados.
        if (builder.Environment.IsEnvironment("Testing"))
        {
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(TestController).Assembly);
    
            // Usa banco de dados em memória para testes
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Scoped);
            
        }
        else
        {
            // Usa o banco de dados MySQL para desenvolvimento e produção
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        // Constrói o aplicativo com base nas configurações definidas.
        var app = builder.Build();
        
        // Configuração do middleware de tratamento de exceções
        app.UseMiddleware<ExceptionMiddleware>();

        // Configuração do pipeline de requisições HTTP.
        if (app.Environment.IsDevelopment())
        {
            // Habilita o Swagger apenas no ambiente de desenvolvimento
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        

        // Desativa o redirecionamento HTTPS no ambiente de testes e desenvolvimento
        if (!app.Environment.IsDevelopment() && !app.Environment.IsEnvironment("Testing"))
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