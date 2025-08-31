# Vemynd Store API

Vemynd Store API é o backend para uma aplicação de e-commerce, construído com .NET 8 e projetado para ser robusto, testável e escalável.

## 🌐 Visão Full-Stack

Este projeto representa a camada de **backend** de uma aplicação completa de e-commerce. A API foi projetada para servir como a fonte de dados e lógica de negócio para uma aplicação **front-end** moderna (planejada com Next.js/React).

O objetivo é criar uma solução full-stack realista, onde o backend e o frontend são desacoplados e se comunicam via uma API RESTful bem definida.

## ✨ Core Technologies

- **.NET 8**: A mais recente versão do framework da Microsoft.
- **ASP.NET Core**: Para a construção da API RESTful.
- **Entity Framework Core 8**: ORM para interação com o banco de dados.
- **MySQL**: Banco de dados relacional.
- **Docker**: Para containerização da aplicação e do banco de dados.
- **AutoMapper**: Para mapeamento entre DTOs e entidades.
- **FluentValidation**: Para validação declarativa e robusta dos dados de entrada.
- **xUnit, Moq & FluentAssertions**: Para uma suíte de testes completa e legível.

## 🏛️ Arquitetura e Conceitos Aplicados

Este projeto é construído sobre vários princípios e padrões de design de software para garantir que seja robusto, manutenível e escalável.

- **Arquitetura Limpa (em Camadas)**: O código é organizado em camadas distintas (API/Controllers, Services, Repositories, Data) para reforçar a Separação de Responsabilidades (SoC).
  - **Camada de API**: Lida com requisições e respostas HTTP.
  - **Camada de Serviço**: Contém toda a lógica de negócio.
  - **Camada de Repositório**: Abstrai o acesso aos dados.

- **Princípios SOLID**: Especialmente o **Princípio da Responsabilidade Única (SRP)**, onde cada classe tem um propósito único e bem definido.

- **Injeção de Dependência (DI)**: Usada extensivamente para desacoplar componentes e facilitar os testes.

- **Padrões Repository e Unit of Work**: O padrão Repository abstrai a camada de dados, e o padrão Unit of Work (fornecido nativamente pelo `DbContext` do EF Core) garante a consistência dos dados agrupando operações em transações únicas.

- **Arquitetura Orientada a Eventos**: Um Event Bus em memória (`IEventBus`) é usado para desacoplar ações primárias (como criar um produto) de efeitos colaterais secundários (como indexar para busca ou enviar notificações). Isso torna o sistema altamente extensível.

## 🚀 Features

- ✅ CRUD completo para as entidades `Product` e `Category`.
- ✅ Validação de regras de negócio (ex: nomes únicos, não permitir exclusão de categorias com produtos).
- ✅ Documentação interativa da API com Swagger/OpenAPI.
- ✅ Tratamento de exceções global e padronizado, retornando `ProblemDetails` (RFC 7807).
- ✅ Suíte de testes abrangente, incluindo testes de unidade e integração.

## 🏁 Getting Started

Siga os passos abaixo para configurar e executar o projeto localmente.

### Pré-requisitos

- .NET 8 SDK
- Docker e Docker Compose

### Configuração

1.  Na pasta `backend`, copie o arquivo de exemplo `.env.example` para um novo arquivo chamado `.env`.
    ```bash
    cp .env.example .env
    ```

2.  Abra o arquivo `.env` e preencha os valores das variáveis de ambiente com suas credenciais. O arquivo `.env` **não deve** ser comitado no Git.

### Executando a Aplicação

#### Opção 1: Usando Docker Compose (Recomendado)

Esta é a maneira mais simples de executar o banco de dados e a API juntos.

1.  Navegue até a pasta `backend` do projeto.
2.  Execute o comando:
    ```bash
    docker-compose up --build
    ```
3.  A API estará disponível em `http://localhost:5000`.

#### Opção 2: Executando Localmente com `dotnet`

1.  Primeiro, inicie apenas o banco de dados usando Docker:
    ```bash
    # Na pasta /backend
    docker-compose up -d mysql_db
    ```
2.  Navegue até a pasta do projeto da API:
    ```bash
    cd VemyndStore.Api
    ```
3.  Aplique as migrações do banco de dados. O `DesignTimeDbContextFactory` garantirá que as variáveis do `.env` sejam usadas.
    ```bash
    dotnet ef database update
    ```
4.  Execute a aplicação:
    ```bash
    dotnet run
    ```

## 📖 Documentação da API

Com a aplicação em execução, acesse a UI interativa do Swagger para explorar e testar os endpoints da API.

- **URL**: http://localhost:5000 (ou a porta configurada)

## 🧪 Executando os Testes

Para executar a suíte de testes completa (unidade e integração), navegue até a pasta `backend` e execute:

```bash
dotnet test
```
