FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia arquivos .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o resto dos arquivos
COPY . ./
# Publica a aplicação
RUN dotnet publish -c Release -o out

# Cria a imagem final para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VemyndStore.Api.dll"]