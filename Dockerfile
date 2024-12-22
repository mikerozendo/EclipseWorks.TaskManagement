# Stage 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-stage
WORKDIR /app

# Copiar arquivos para o container
COPY . ./

# Restaurar dependências (usa o .sln para restaurar os pacotes necessários)
RUN dotnet restore EclipseWorks.TaskManagement.sln

# Publicar os artefatos otimizados (binários para produção)
RUN dotnet publish src/EclipseWorks.TaskManagement.WebApi/EclipseWorks.TaskManagement.WebApi.csproj -c Release -o /publish

# Stage 2: Configuração do ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-stage
WORKDIR /app

# Configurar variáveis de ambiente necessárias
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080 

# Expor a porta 8080 para comunicação externa
EXPOSE 8080

# Copiar artefatos da fase de build
COPY --from=build-stage /publish .

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "EclipseWorks.TaskManagement.WebApi.dll"]