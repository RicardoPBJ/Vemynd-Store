using Microsoft.EntityFrameworkCore;
using VemyndStore.Api.Data;

// Criação do builder para configurar e construir o aplicativo web.
var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner de injeção de dependência.
// O AddControllers registra os controladores para lidar com as requisições HTTP.
builder.Services.AddControllers();

// Configuração para habilitar a documentação Swagger/OpenAPI.
// O AddEndpointsApiExplorer fornece suporte para explorar os endpoints da API.
// O AddSwaggerGen gera a documentação da API automaticamente.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext para acesso ao banco de dados.
// Registra o ApplicationDbContext como um serviço que pode ser injetado em outras partes da aplicação.
// Usa o MySQL como banco de dados, com a string de conexão definida no arquivo de configuração.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Constrói o aplicativo com base nas configurações definidas.
var app = builder.Build();

// Configuração do pipeline de requisições HTTP.
// Verifica se o ambiente é de desenvolvimento para habilitar o Swagger e a interface SwaggerUI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o middleware do Swagger para gerar a documentação.
    app.UseSwaggerUI(); // Habilita a interface gráfica do Swagger para explorar a API.
}

// Habilita o redirecionamento de requisições HTTP para HTTPS.
app.UseHttpsRedirection();

// Habilita a autorização para proteger os endpoints da API.
app.UseAuthorization();

// Mapeia os controladores para os endpoints definidos nos atributos dos controladores.
app.MapControllers();

// Inicia o aplicativo e começa a escutar requisições.
app.Run();

